package vrp.jee.domain;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlTransient;

/**
 *
 * @author Maciej Radzikowski <maciej@radzikowski.com.pl>
 */
public class City extends AbsLocation {

	@XmlTransient
	private Depot depot;

	private Integer demand = 0;

	private Boolean isFirst = false;

	private Boolean isLast = false;

	private Boolean isStop = false;

	@XmlElement
	public Integer getDepotId() {
		if (depot != null) {
			return depot.getId();
		}
		return null;
	}

	public Depot getDepot() {
		return depot;
	}

	public void setDepot(Depot depot) {
		this.depot = depot;
	}

	public Integer getDemand() {
		return demand;
	}

	public void setDemand(Integer demand) {
		this.demand = demand;
	}

	public Boolean getIsFirst() {
		return isFirst;
	}

	public void setIsFirst(Boolean isFirst) {
		this.isFirst = isFirst;
	}

	public Boolean getIsLast() {
		return isLast;
	}

	public void setIsLast(Boolean isLast) {
		this.isLast = isLast;
	}

	public Boolean getIsStop() {
		return isStop;
	}

	public void setIsStop(Boolean isStop) {
		this.isStop = isStop;
	}

}
