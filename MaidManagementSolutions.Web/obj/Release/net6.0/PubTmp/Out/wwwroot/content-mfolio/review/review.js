document.addEventListener("DOMContentLoaded", function() {
    const stars = document.querySelectorAll(".star");
    const ratingValue = document.querySelector("#rating-value");
    let rating = 0;

    stars.forEach(function(star, index) {
        star.addEventListener("click", function() {
            rating = index + 1;
            ratingValue.value = rating;
            stars.forEach(function(s, i) {
                if (i <= index) {
                    s.classList.add("active");
                } else {
                    s.classList.remove("active");
                }
            });
        });
    });

    const form = document.querySelector("#review-form");
    form.addEventListener("submit", function(event) {
        event.preventDefault();
        const name = document.querySelector("#name").value;
        const comment = document.querySelector("#comment").value;
        if (!name || !comment || rating === 0) {
            alert("Please fill in all fields and provide a rating.");
            return;
        }
        const review = document.createElement("div");
        review.classList.add("review");
        review.innerHTML = `
            <strong>${name}</strong> rated it ${rating} stars<br>
            <em>${comment}</em>
        `;
        document.querySelector("#reviews-container").appendChild(review);
        resetForm();
    });

    function resetForm() {
        document.querySelector("#name").value = "";
        document.querySelector("#comment").value = "";
        rating = 0;
        stars.forEach(function(s) {
            s.classList.remove("active");
        });
    }
});
