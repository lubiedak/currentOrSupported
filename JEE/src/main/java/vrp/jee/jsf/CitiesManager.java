package vrp.jee.jsf;

import java.io.Serializable;
import java.util.List;
import javax.ejb.EJB;
import javax.enterprise.context.SessionScoped;
import javax.faces.application.FacesMessage;
import javax.faces.context.FacesContext;
import javax.inject.Named;
import vrp.jee.domain.City;
import vrp.jee.domain.Input;
import vrp.jee.ejb.JenkinsClientBean;

/**
 * @author Maciej Radzikowski <maciej@radzikowski.com.pl>
 */
@Named
@SessionScoped
public class CitiesManager implements Serializable {

	private static final long serialVersionUID = 1L;

	@EJB
	private JenkinsClientBean jenkinsClientBean;

	private Input input = new Input();

	private City city = new City();

	public void add() {
		input.addCity(city);
		city = new City();
	}

	public void remove() {
		input.removeCity(city);
		city = new City();
	}

	public String startAlgorithm() {
		int jobId = jenkinsClientBean.addInput(input);
		input = new Input();

		boolean start = jenkinsClientBean.startJob(jobId);

		if (start) {
			return "processing";
		} else {
			FacesContext context = FacesContext.getCurrentInstance();
			context.addMessage(null, new FacesMessage("Could not start Jenkins job"));
			return "processingError";
		}
	}

	public City getCity() {
		return city;
	}

	public void setCity(City city) {
		this.city = city;
	}

	public List<City> getCities() {
		return input.getCities();
	}
}
