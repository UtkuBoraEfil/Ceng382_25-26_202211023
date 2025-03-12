document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("classForm");
    const tableBody = document.querySelector("#classTable tbody");

    form.addEventListener("submit", function (event) {
        event.preventDefault();

        // Get input values
        const className = document.getElementById("className").value;
        const numPeople = document.getElementById("numPeople").value;
        const description = document.getElementById("description").value;

        // Create table row
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${className}</td>
            <td>${numPeople}</td>
            <td>${description}</td>
            <td><button class="delete-btn">❌</button></td>
        `;

        // Append row to table
        tableBody.appendChild(row);

        // Clear form fields
        form.reset();

        // 🔹 Focus Event: Highlight input field when focused
        document.querySelectorAll("input, textarea").forEach(input => {
            input.addEventListener("focus", function () {
                this.style.border = "2px solid blue";
            });

            input.addEventListener("blur", function () {
                this.style.border = "1px solid #ccc";
            });
        });

        // 🔹 Click Event: Remove row on delete button click
        row.querySelector(".delete-btn").addEventListener("click", function () {
            row.remove();
        });

        // 🔹 Mouseover Event: Change row background color on hover
        row.addEventListener("mouseenter", () => {
            row.style.backgroundColor = "#f0f0f0";
        });

        row.addEventListener("mouseleave", () => {
            row.style.backgroundColor = "white";
        });

        // 🔹 Row Click Event: Log details & highlight row
        row.addEventListener("click", function () {
            console.log(`Class: ${className}, People: ${numPeople}, Description: ${description}`);
            
            // Highlight row when clicked
            document.querySelectorAll("tr").forEach(tr => tr.classList.remove("highlight"));
            row.classList.add("highlight");
        });
    });
});
