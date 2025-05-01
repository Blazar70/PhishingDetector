
function changeBanner() {
    document.getElementById("project-image").src = "https://assets-v2.lottiefiles.com/a/44e7fabc-1152-11ee-b2ae-dfe60e59d52d/5PONYiprvl.gif" ;
    document.getElementById("dynamic-text").textContent = "Scanning for phishing threats...";
}

function validateSignupForm(event) {
    var password = document.getElementById("password").value;
    var confirm = document.getElementById("confirm-password").value;

    if (password !== confirm) {
        alert("Passwords do not match!");
        event.preventDefault();
    }
}

function addScanResult() {
    var table = document.getElementById("scan-table");
    if (!table) return;

    var row = table.insertRow();
    row.innerHTML = `
        <td>https://newscan.com</td>
        <td>Safe</td>
        <td>${new Date().toLocaleDateString()}</td>
    `;
}

window.onload = function () {
    const img = document.getElementById("project-image");
    if (img) {
        img.addEventListener("click", changeBanner);
    }

    const signupForm = document.querySelector("form");
    if (signupForm && signupForm.querySelector("#confirm-password")) {
        signupForm.addEventListener("submit", validateSignupForm);
    }

    const historyBtn = document.getElementById("add-result-btn");
    if (historyBtn) {
        historyBtn.addEventListener("click", addScanResult);
    }
};
