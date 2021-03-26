let autocomplete;

// Center of Philadelphia, will be used to bias search results
const center = { lat: 39.951668, lng: -75.163743 };
// Create a bounding box with sides ~20km away from the center point
const defaultBounds = {
    north: center.lat + 0.2,
    south: center.lat - 0.2,
    east: center.lng + 0.2,
    west: center.lng - 0.2,
};

// Options for Autocomplete constructor
const options = {
    bounds: defaultBounds,
    componentRestrictions: { country: "us" },
    fields: ["formatted_address"],
    origin: center,
    strictBounds: true
}

// Callback function for api
function initMap() {
    autocomplete = new google.maps.places.Autocomplete(document.getElementById("MainContent_txtAddress"), options);

    autocomplete.addListener('place_changed', function () {
        const place = autocomplete.getPlace();

        // Set hidden variable to place result to retrieve in serverside code
        document.getElementById("hdnfldFormattedAddress").value = place.formatted_address;
    });

};