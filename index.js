const credentials = [];

function storeCredentials(event) {
    event.preventDefault();
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    credentials.push({ username, password });
    console.log(credentials);
}

var clockElement = document.getElementById('clock');

function clock() {
    const today = new Date();
    let hours = today.getHours();
    let minutes = today.getMinutes();
    let seconds = today.getSeconds();

    // Format the time string
    let timeString = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;

    // Set the time string in the HTML
    clockElement.textContent = timeString;
}


// Update the clock every second
setInterval(clock, 1000);

document.addEventListener('keydown', function(event) {
    if (event.key === 'h') {
        const element = document.getElementById('loginForm');
        if (element) {
            element.classList.toggle('hidden');
        }
    }
});