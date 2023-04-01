// check if input from the password is the same

document.getElementById("password1").addEventListener("keyup", testpassword);
document.getElementById("password2").addEventListener("keyup", testpassword);

function testpassword() {
	let text1 = document.getElementById("password1");
	let text2 = document.getElementById("password2");

	if ((text1.value.length >= 3 && text2.value.length >= 3) && (text1.value == text2.value)) {
		text1.style.borderColor = "green";
		text2.style.borderColor = "green";
	} else {
		text1.style.borderColor = "red";
		text2.style.borderColor = "red";
	}
}

// check if email is the right format

document.getElementById("email").addEventListener("keyup", testemail);

function testemail() {
let text = document.getElementById("email");
	let regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
	if (regex.test(text.value)) {
		text.style.borderColor = "green";
	} else {
		text.style.borderColor = "red";
	}
}