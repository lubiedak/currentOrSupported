package vrp.jee.domain;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author Maciej Radzikowski <maciej@radzikowski.com.pl>
 */
@XmlRootElement
@XmlAccessorType(XmlAccessType.FIELD)
public class Point {

	private Integer x;

	private Integer y;

	public Point() {
	}

	public Point(Integer x, Integer y) {
		this.x = x;
		this.y = y;
	}

	public Integer getX() {
		return x;
	}

	public void setX(Integer x) {
		this.x = x;
	}

	public Integer getY() {
		return y;
	}

	public void setY(Integer y) {
		this.y = y;
	}
    
}
