package vrp.jee.rest;

import java.util.logging.Level;
import java.util.logging.Logger;
import javax.ejb.EJB;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.WebApplicationException;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import vrp.jee.domain.Input;
import vrp.jee.ejb.JenkinsClientBean;

/**
 * REST facade for communication with Jenkins.
 *
 * Manages sending input and receiving output in JSON format.
 *
 * @author Maciej Radzikowski <maciej@radzikowski.com.pl>
 */
@Path("jenkins/{id}")
public class JenkinsFacade {

	private static final Logger log = Logger.getLogger(JenkinsFacade.class.getName());

	@EJB
	private JenkinsClientBean jenkinsClientBean;

	@GET
	@Path("input")
	@Produces(MediaType.APPLICATION_JSON)
	public Input input(@PathParam("id") int id) {
		log.log(Level.INFO, "Jenkins input requested with job id: {0}", id);
		Input input = jenkinsClientBean.getWaitingInputs().remove(id);
		if (input != null) {
			return input;
		} else {
			log.log(Level.WARNING, "Job {0} not found.", id);
			throw new WebApplicationException(Response.status(Response.Status.NOT_FOUND).build());
		}
	}

	@POST
	@Path("output")
	@Consumes(MediaType.APPLICATION_JSON)
	public Response output(@PathParam("id") int id) {
		log.log(Level.INFO, "Jenkins output received for job id: {0}", id);
		return Response.ok().build();
	}

}
