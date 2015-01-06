package vrp.jee.domain;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author Maciej Radzikowski <maciej@radzikowski.com.pl>
 */
@XmlRootElement
@XmlAccessorType(XmlAccessType.FIELD)
public class Input {

	private final List<Depot> depots = new ArrayList<>();

	private final List<City> cities = new ArrayList<>();

	public List<Depot> getDepots() {
		return depots;
	}

	public void addDepot(Depot depot) {
		depots.add(depot);
	}
	
	public void removeDepot(Depot depot) {
		depots.remove(depot);
	}

	public List<City> getCities() {
		return cities;
	}

	public void addCity(City city) {
		cities.add(city);
	}
	
	public void removeCity(City city) {
		cities.remove(city);
	}

}
