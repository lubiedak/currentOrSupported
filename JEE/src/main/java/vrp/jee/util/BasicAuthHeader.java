package vrp.jee.util;

import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import org.apache.commons.codec.binary.Base64;

/**
 * Request header that encodes username and password for HTTP Basic Authentication.
 *
 * If no charset is set, the default ISO_8859_1 is used ({@link StandardCharsets.ISO_8859_1});
 * 
 * Example usage:
 * <code>
 * ClientBuilder.newClient()
 *	.target("http://example.com")
 *	.request()
 *	.header("Authentication", new BasicAuthHeader("username", "password"))
 *	.get();
 * </code>
 * @see <a href="http://en.wikipedia.org/wiki/Basic_access_authentication">http://en.wikipedia.org/wiki/Basic_access_authentication</a>
 * @author Maciej Radzikowski <maciej@radzikowski.com.pl>
 */
public class BasicAuthHeader {

	private String username;
	private String password;

	private Charset charset = StandardCharsets.ISO_8859_1;

	public BasicAuthHeader() {
	}

	public BasicAuthHeader(String username, String password) {
		this.username = username;
		this.password = password;
	}

	public BasicAuthHeader(String username, String password, Charset charset) {
		this(username, password);
		this.charset = charset;
	}

	@Override
	public String toString() {
		String auth = username + ":" + password;
		byte[] encodedBytes = Base64.encodeBase64(auth.getBytes());
		String encodedString = new String(encodedBytes, charset);
		return "Basic " + encodedString;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public void setCharset(Charset charset) {
		this.charset = charset;
	}

}
