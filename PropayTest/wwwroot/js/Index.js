/*document.addEventListener('DOMContentLoaded', (event) => {
    const form = document.getElementById('loginForm');
    const alertMessage = document.getElementById('alertMessage');

    form.addEventListener('submit', function (e) {
        alert("form submit!!!")
        
        const username = form.querySelector('input[name="Username"]').value.trim();
        const password = form.querySelector('input[name="Password"]').value.trim();

        if (!username || !password) {
            e.preventDefault(); // Prevent form submission
            alertMessage.textContent = 'Please fill in both fields.';
            alertMessage.className = 'alert alert-danger'; // Add class for styling
        }
    });
});
*/
alert("alert");