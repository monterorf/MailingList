

const form = document.querySelector("#myForm");


form.addEventListener("submit", function (event) {
	// stop form submission
	event.preventDefault();


	// validate the form
	let FirstName = form.elements["FirstName"].value;
    let LastName = form.elements["LastName"].value;
	let Email = form.elements["Email"].value;
    let formData = new FormData();
    formData.append('FirstName', FirstName);
    formData.append('LastName', LastName);
    formData.append('Email', Email);
    

    fetch('https://localhost:44384/api/User', {
        method: 'POST',
        body: formData
})   
            .then(function (response) {
	// The API call was successful!
    console.log(response)
	return response.status;
}).then(function (data) {
	// This is the JSON from our response
	if(data==200)
    {
        alert("User has been added successfully");
        document.getElementById("myForm").reset();
    }
    else{
        alert("Something went wrong");
    }
}).catch(function (err) {
	// There was an error
	console.warn('Something went wrong.', err);
});
});
