
(() => {
    "use strict";

    function ready(callback) {
        if (document.readyState === "loading") {
            document.addEventListener(
                "DOMContentLoaded",
                callback,
                { once: true }
            );
        } else {
            callback();
        }
    }

    /* =========================================================
       VISIBILITY HELPERS
       ========================================================= */

    function markVisible(element) {
        if (!element) return;

        element.classList.add("is-visible");

        element.style.setProperty("opacity", "1", "important");
        element.style.setProperty("visibility", "visible", "important");
        element.style.setProperty("transform", "none", "important");
        element.style.setProperty("filter", "none", "important");
    }

    function showHeroImmediately() {
        const heroSelectors = [
            ".hero",
            ".hero-copy",
            ".hero-art",
            ".hero-bottom-line",
            ".hero-content",
            ".hero h1",
            ".hero h2",
            ".hero p",
            ".hero a",
            ".hero button",
            ".hero img",
            ".hero picture",
            ".hero .reveal"
        ];

        document
            .querySelectorAll(heroSelectors.join(","))
            .forEach(markVisible);
    }

    /* =========================================================
       REVEAL ANIMATION FALLBACK
       ========================================================= */

    function setupRevealAnimations() {
        const revealElements = document.querySelectorAll(
            ".reveal, .home-projects, .home-team-section"
        );

        if (!revealElements.length) return;

        if (!("IntersectionObserver" in window)) {
            revealElements.forEach(markVisible);
            return;
        }

        const observer = new IntersectionObserver(
            (entries, currentObserver) => {
                entries.forEach(entry => {
                    if (!entry.isIntersecting) return;

                    markVisible(entry.target);
                    currentObserver.unobserve(entry.target);
                });
            },
            {
                threshold: 0.05,
                rootMargin: "0px 0px 60px 0px"
            }
        );

        revealElements.forEach(element => {
            // Never wait for scrolling before displaying the hero.
            if (element.closest(".hero")) {
                markVisible(element);
                return;
            }

            observer.observe(element);
        });
    }

    /* =========================================================
       PROJECT HOVER STATES
       ========================================================= */

    function setupProjects() {
        const projects = document.querySelectorAll(
            ".project-card, .home-project-card, .project-item"
        );

        projects.forEach((project, index) => {
            project.style.setProperty(
                "--reveal-delay",
                `${index * 80}ms`
            );

            project.addEventListener("mouseenter", () => {
                project.classList.add("is-hovered");
            });

            project.addEventListener("mouseleave", () => {
                project.classList.remove("is-hovered");
            });
        });
    }

    /* =========================================================
       TEAM HOVER STATES
       ========================================================= */

    function setupTeam() {
        const teamMembers = document.querySelectorAll(
            ".team-card, .team-member, .home-team-card"
        );

        teamMembers.forEach(member => {
            member.addEventListener("mouseenter", () => {
                member.classList.add("is-hovered");
            });

            member.addEventListener("mouseleave", () => {
                member.classList.remove("is-hovered");
            });
        });
    }

    /* =========================================================
       INTERNAL ANCHOR NAVIGATION
       ========================================================= */

    function setupContactLinks() {
        document.querySelectorAll('a[href^="#"]').forEach(link => {
            const href = link.getAttribute("href");

            // These are handled by main.js.
            if (
                link.matches(
                    '.home-project-cta a[href="#contact"], ' +
                    '.home-team-cta a[href="#contact"]'
                )
            ) {
                return;
            }

            if (!href || href === "#") return;

            let target;

            try {
                target = document.querySelector(href);
            } catch {
                return;
            }

            if (!target) return;

            link.addEventListener("click", event => {
                // Allow normal browser behavior for modified clicks.
                if (
                    event.ctrlKey ||
                    event.metaKey ||
                    event.shiftKey ||
                    event.altKey
                ) {
                    return;
                }

                event.preventDefault();

                const reducedMotion = window.matchMedia(
                    "(prefers-reduced-motion: reduce)"
                ).matches;

                target.scrollIntoView({
                    behavior: reducedMotion ? "auto" : "smooth",
                    block: "start"
                });

                if (history.replaceState) {
                    history.replaceState(null, "", href);
                }
            });
        });
    }

    /* =========================================================
       BACK TO TOP
       ========================================================= */

    function setupBackToTop() {
        const backToTop = document.querySelector(
            "#backToTop, .back-to-top, .scroll-top"
        );

        if (!backToTop) return;

        function updateButton() {
            const shouldShow = window.scrollY > 300;

            backToTop.style.opacity = shouldShow ? "1" : "0";
            backToTop.style.visibility = shouldShow
                ? "visible"
                : "hidden";

            backToTop.style.pointerEvents = shouldShow
                ? "auto"
                : "none";
        }

        window.addEventListener("scroll", updateButton, {
            passive: true
        });

        updateButton();

        backToTop.addEventListener("click", event => {
            event.preventDefault();

            const reducedMotion = window.matchMedia(
                "(prefers-reduced-motion: reduce)"
            ).matches;

            window.scrollTo({
                top: 0,
                behavior: reducedMotion ? "auto" : "smooth"
            });
        });
    }

    /* =========================================================
       COUNTERS
       ========================================================= */

    function setupCounters() {
        document.querySelectorAll("[data-count]").forEach(counter => {
            const target = Number(
                counter.getAttribute("data-count")
            );

            if (!Number.isFinite(target)) return;

            counter.textContent = String(target);
        });
    }

    /* =========================================================
       INITIALIZATION
       ========================================================= */

    function initialize() {
        // Run first so the hero is never left hidden.
        showHeroImmediately();

        setupRevealAnimations();
        setupProjects();
        setupTeam();
        setupContactLinks();
        setupBackToTop();
        setupCounters();
    }

    ready(initialize);

    // Recover the hero if another script modifies its visibility.
    window.addEventListener(
        "load",
        showHeroImmediately,
        { once: true }
    );
})();


    
/* =========================================================
   TECHNINXA CUSTOM CURSOR
   Add this at the bottom of home-interactions.js
========================================================= */
(() => {
    "use strict";

    function initTechninxaCursor() {
        // Prevent duplicate cursor elements if this script initializes twice.
        if (document.querySelector(".custom-cursor")) return;

        // Custom cursors are intended only for mouse/trackpad devices.
        if (!window.matchMedia("(hover: hover) and (pointer: fine)").matches) {
            return;
        }

        if (!document.body) return;

        const cursor = document.createElement("div");
        cursor.className = "custom-cursor";
        cursor.setAttribute("aria-hidden", "true");

        const dot = document.createElement("span");
        dot.className = "custom-cursor-dot";
        cursor.appendChild(dot);

        document.body.appendChild(cursor);

        let mouseX = -100;
        let mouseY = -100;
        let frameRequested = false;

        function moveCursor() {
            cursor.style.transform =
                `translate3d(${mouseX}px, ${mouseY}px, 0)`;

            frameRequested = false;
        }

        document.addEventListener("pointermove", (event) => {
            if (event.pointerType && event.pointerType !== "mouse") return;

            mouseX = event.clientX;
            mouseY = event.clientY;

            cursor.classList.add("is-active");

            if (!frameRequested) {
                frameRequested = true;
                window.requestAnimationFrame(moveCursor);
            }
        }, { passive: true });

        document.addEventListener("pointerover", (event) => {
            if (event.pointerType && event.pointerType !== "mouse") return;

            const target = event.target;

            if (!(target instanceof Element)) return;

            const interactive = target.closest(
                "a, button, input, textarea, select, " +
                "[role='button'], [data-cursor-hover]"
            );

            cursor.classList.toggle("is-hovering", Boolean(interactive));
        }, { passive: true });

        document.addEventListener("pointerdown", (event) => {
            if (event.pointerType === "mouse") {
                cursor.classList.add("is-clicking");
            }
        });

        document.addEventListener("pointerup", () => {
            cursor.classList.remove("is-clicking");
        });

        document.addEventListener("pointerout", (event) => {
            // Hide when the pointer leaves the browser viewport.
            if (!event.relatedTarget) {
                cursor.classList.remove("is-active", "is-hovering", "is-clicking");
            }
        });

        window.addEventListener("blur", () => {
            cursor.classList.remove("is-active", "is-hovering", "is-clicking");
        });

        document.addEventListener("visibilitychange", () => {
            if (document.hidden) {
                cursor.classList.remove("is-active", "is-hovering", "is-clicking");
            }
        });

        // Keep the native cursor if JavaScript fails to initialize fully.
        document.documentElement.classList.add("techninxa-custom-cursor-ready");
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", initTechninxaCursor, {
            once: true
        });
    } else {
        initTechninxaCursor();
    }
})();
