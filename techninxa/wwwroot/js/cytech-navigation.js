/* Corporate navigation menu and the IT/IoT floating section rail. */
(() => {
    "use strict";
    function ready() {
        const toggle = document.querySelector(".site-header .menu-toggle");
        const menu = document.getElementById("corporateNav");
        if (toggle && menu) {
            const setOpen = open => {
                menu.classList.toggle("open", open);
                toggle.setAttribute("aria-expanded", open ? "true" : "false");
                document.body.classList.toggle("menu-open", open);
            };
            toggle.addEventListener("click", () => setOpen(!menu.classList.contains("open")));
            menu.querySelectorAll("a").forEach(link =>
                link.addEventListener("click", () => setOpen(false))
            );
            document.addEventListener("keydown", event => {
                if (event.key === "Escape") setOpen(false);
            });
            document.addEventListener("click", event => {
                if (!menu.contains(event.target) && !toggle.contains(event.target)) setOpen(false);
            });
            window.addEventListener("resize", () => {
                if (window.innerWidth > 1240) setOpen(false);
            });
        }

        const rail = document.querySelector(".cy-it-rail");
        if (!rail) return;
        const links = [...rail.querySelectorAll("[data-it-section]")];
        const isProjectsPage = window.location.pathname.toLowerCase().startsWith("/projects");

        const setActive = name => {
            for (const link of links) {
                const active = link.dataset.itSection === name;
                link.classList.toggle("is-active", active);
                if (active) link.setAttribute("aria-current", "location");
                else link.removeAttribute("aria-current");
            }
        };

        let pending = false;
        const updateRail = () => {
            pending = false;
            if (isProjectsPage) { setActive("projects"); return; }

            let active = "home";
            const names = ["services", "projects", "about", "contact"];
            const threshold = Math.max(120, window.innerHeight * 0.38);
            for (const name of names) {
                const target = document.getElementById(name);
                if (target && target.getBoundingClientRect().top <= threshold) active = name;
            }
            const hash = window.location.hash.slice(1).toLowerCase();
            if (names.includes(hash)) {
                const element = document.getElementById(hash);
                if (element && Math.abs(element.getBoundingClientRect().top) < 220) active = hash;
            }
            setActive(active);
        };
        const scheduleUpdate = () => {
            if (pending) return;
            pending = true;
            window.requestAnimationFrame(updateRail);
        };
        window.addEventListener("scroll", scheduleUpdate, { passive: true });
        window.addEventListener("hashchange", scheduleUpdate);
        window.addEventListener("resize", scheduleUpdate);
        scheduleUpdate();
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", ready, { once: true });
    } else {
        ready();
    }
})();
