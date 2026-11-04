
(() => {
    const root = document.querySelector(".person-portfolio");
    if (!root) return;

    const reducedMotion = window.matchMedia(
        "(prefers-reduced-motion: reduce)"
    ).matches;

    // Scroll reveal: reveal each section as it enters the viewport.
    const revealElements = root.querySelectorAll(".pp-reveal");

    if ("IntersectionObserver" in window && !reducedMotion) {
        const revealObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach(entry => {
                if (!entry.isIntersecting) return;

                entry.target.classList.add("is-visible");
                observer.unobserve(entry.target);
            });
        }, {
            threshold: 0.12,
            rootMargin: "0px 0px -35px 0px"
        });

        revealElements.forEach(element => revealObserver.observe(element));
    } else {
        revealElements.forEach(element => element.classList.add("is-visible"));
    }

    // Animated role text.
    const roleElement = root.querySelector("#pp-rotating-role");
    const roles = [
        "technology.",
        "thoughtful design.",
        "digital experiences.",
        "solving problems."
    ];

    if (roleElement && !reducedMotion) {
        let roleIndex = 0;

        window.setInterval(() => {
            roleElement.classList.add("is-changing");

            window.setTimeout(() => {
                roleIndex = (roleIndex + 1) % roles.length;
                roleElement.textContent = roles[roleIndex];
                roleElement.classList.remove("is-changing");
            }, 200);
        }, 2600);
    }

    // Subtle pointer-based parallax on desktop.
    const parallaxElements = root.querySelectorAll(".pp-parallax");
    const finePointer = window.matchMedia("(pointer: fine)").matches;

    if (!reducedMotion && finePointer && parallaxElements.length) {
        let frameRequested = false;
        let pointerX = 0;
        let pointerY = 0;

        root.addEventListener("pointermove", event => {
            pointerX = (event.clientX / window.innerWidth - 0.5) * 2;
            pointerY = (event.clientY / window.innerHeight - 0.5) * 2;

            if (frameRequested) return;
            frameRequested = true;

            window.requestAnimationFrame(() => {
                parallaxElements.forEach(element => {
                    const depth = Number(element.dataset.depth) || 0.02;
                    const moveX = pointerX * depth * 180;
                    const moveY = pointerY * depth * 180;

                    element.style.transform =
                        `translate3d(${moveX}px, ${moveY}px, 0)`;
                });

                frameRequested = false;
            });
        });

        root.addEventListener("pointerleave", () => {
            parallaxElements.forEach(element => {
                element.style.transform = "";
            });
        });
    }
})();
