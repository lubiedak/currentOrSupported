package vrp.jee.ejb;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.annotation.PostConstruct;
import javax.annotation.PreDestroy;
import javax.ejb.Singleton;
import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.Entity;
import javax.ws.rs.core.Form;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;
import vrp.jee.Config;
import vrp.jee.domain.AbsLocation;
import vrp.jee.domain.Coordinate;
import vrp.jee.domain.Input;
import vrp.jee.domain.Point;
import vrp.jee.util.BasicAuthHeader;

/**
 * @author Maciej Radzikowski <maciej@radzikowski.com.pl>
 */
@Singleton
public class JenkinsClientBean {

    private static final Logger log = Logger.getLogger(JenkinsClientBean.class.getName());

    protected Client client;

    private final Random random = new Random();

    /**
     * Map of job id => input to be send to Jenkins.
     */
    private final Map<Integer, Input> waitingInputs = new HashMap<>();

    @PostConstruct
    private void init() {
        client = ClientBuilder.newClient();
    }

    @PreDestroy
    private void clean() {
        client.close();
    }

    /**
     * Generates random job's id and puts it in waiting inputs map.
     *
     * @param input
     * @return Job id.
     */
    public int addInput(Input input) {
        int id;
        do {
            id = Math.abs(random.nextInt());
        } while (waitingInputs.containsKey(id));

        calculatePoints(input);

        log.log(Level.INFO, "Putting new input with generated id: {0}", id);
        waitingInputs.put(id, input);

        return id;
    }

    /**
     * Calculates points from coordinates for all cities and depots.
     */
    private void calculatePoints(Input input) {
        List<AbsLocation> locations = new ArrayList<>();
        locations.addAll(input.getDepots());
        locations.addAll(input.getCities());

        Double minLat = locations.get(0).getCoordinate().getLatitude();
        Double minLong = locations.get(0).getCoordinate().getLongitude();
        for (int i = 1; i < locations.size(); i++) {
            Coordinate coordinate = locations.get(i).getCoordinate();
            if (coordinate.getLatitude() < minLat) {
                minLat = coordinate.getLatitude();
            }
            if (coordinate.getLongitude() < minLong) {
                minLong = coordinate.getLongitude();
            }
        }

        for (AbsLocation location : locations) { // TODO Calculating could be done to calc only latitude distance and only longitude distance, not real distance with same lat/long coordinate
            int distanceX = calculateDistance(new Coordinate(location.getCoordinate().getLatitude(), minLong), location.getCoordinate());
            int distanceY = calculateDistance(new Coordinate(minLat, location.getCoordinate().getLongitude()), location.getCoordinate());
            location.setPoint(new Point(distanceX, distanceY));
        }
    }

    /**
     * @param a
     * @param b
     * @return Distance in kilometers.
     */
    private int calculateDistance(Coordinate a, Coordinate b) {
        float pk = (float) (180 / Math.PI);

        double a1 = a.getLatitude() / pk;
        double a2 = a.getLongitude() / pk;
        double b1 = b.getLatitude() / pk;
        double b2 = b.getLongitude() / pk;

        double t1 = Math.cos(a1) * Math.cos(a2) * Math.cos(b1) * Math.cos(b2);
        double t2 = Math.cos(a1) * Math.sin(a2) * Math.cos(b1) * Math.sin(b2);
        double t3 = Math.sin(a1) * Math.sin(b1);
        double tt = Math.acos(t1 + t2 + t3);

        return (int) (6366 * tt);
    }

    /**
     * Starts Jenkins job for {@link Input} with given ID.
     *
     * @param jobId
     * @return
     */
    public boolean startJob(int jobId) {
        log.log(Level.INFO, "Starting Jenkins job with id: {0}", jobId);

        Form form = new Form();
        form.param("input", Config.APP_BASE_REST_URL + "jenkins/" + jobId + "/input/");
        form.param("output", Config.APP_BASE_REST_URL + "jenkins/" + jobId + "/output/");

        Response response = client
                .target(Config.JENKINS_RUN_URL)
                .request(MediaType.APPLICATION_FORM_URLENCODED)
                .header("Authorization", new BasicAuthHeader(Config.JENKINS_USER, Config.JENKINS_PASS))
                .post(Entity.form(form), Response.class);

        log.log(Level.INFO, "Response for job with id {0}: {1} - {2}, location: {3}", new Object[]{jobId, response.getStatus(), response.getStatusInfo(), response.getLocation()});

        return response.getStatus() == Status.CREATED.getStatusCode();
    }

    public Map<Integer, Input> getWaitingInputs() {
        return waitingInputs;
    }
}
