var googleMap = new function () {
	var map;
	var marker;
	var geocoder;

	this.initialize = function () {
		var mapProp = {
			center: new google.maps.LatLng(52.0, 19.0),
			zoom: 6,
			mapTypeId: google.maps.MapTypeId.ROADMAP
		};
		map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

		marker = new google.maps.Marker({
			draggable: true
		});

		geocoder = new google.maps.Geocoder();

		google.maps.event.addListener(marker, 'dragend', function (e) {
			geocoder.geocode({latLng: e.latLng}, function (results, status) {
				if (status === google.maps.GeocoderStatus.OK) {
					googleMap.updateForm(results[0].geometry.location, results[0].formatted_address);
				}
			});
		});
	};

	this.searchAddress = function (address) {
		this.hideMarker();
		geocoder.geocode({address: address}, function (results, status) {
			if (status === google.maps.GeocoderStatus.OK) {
				if (results.length > 1) {
					$ul = $("ul#alternativeResults");
					for (var i = 0; i < results.length; i++) {
						$ul.append("<li><a href=\"#\" data-lng=\"" + results[i].geometry.location.B + "\" data-lat=\"" + results[i].geometry.location.k + "\">" + googleMap.parseAddress(results[i].formatted_address) + "</a></li>");
					}
					$ul.children("li").first().addClass("selected");
				}
				googleMap.markLocation(results[0].geometry.location, googleMap.parseAddress(results[0].formatted_address));
			} else {
				alert("Nie znalazłem podanego adresu!");
			}
		});
	};

	this.markLocation = function (location, address) {
		map.setCenter(location);
		googleMap.showMarker(location);
		googleMap.updateForm(location, address);
	};

	this.showMarker = function (latLng) {
		marker.setPosition(latLng);
		marker.setMap(map);
	};

	this.hideMarker = function () {
		marker.setMap(null);
	};

	this.updateForm = function (location, fullAddress) {
		$('#cityEdit\\:cityName').val(googleMap.parseAddress(fullAddress));
		$('#cityEdit\\:cityLat').val(location.lat());
		$('#cityEdit\\:cityLong').val(location.lng());
	};
	
	this.reset = function() {
		$('#cityEdit')[0].reset();
		googleMap.hideMarker();
		$("#alternativeResults").empty();
	};

	this.parseAddress = function (fullAddress) {
		if (fullAddress.lastIndexOf(",") > 0) {
			fullAddress = fullAddress.substring(0, fullAddress.lastIndexOf(','));
		}
		return fullAddress;
	};
};

$("#addressSearch").submit(function (e) {
	googleMap.reset();
	googleMap.searchAddress($(this).find('input[type="text"]').val() + ", Polska");
	e.preventDefault();
});

$("#alternativeResults").on("click", "a", function (e) {
	$this = $(this);
	$this.parent().siblings().removeClass("selected");
	$this.parent().addClass("selected");

	var latLng = {lat: $this.data("lat"), lng: $this.data("lng")};
	googleMap.markLocation(latLng, $this.text());

	e.preventDefault();
});

$("#cityEdit .add, #cityEdit .remove").click(function () {
	googleMap.reset();
});

googleMap.initialize();