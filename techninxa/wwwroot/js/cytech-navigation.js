/* Corporate navigation menu and the IT/IoT floating section rail. */
(() => {
    "use strict";
    function ready() {
        const toggle = document.querySelector(".site-header .menu-toggle");
        const menu = document.getElementById("corporateNav");
        if (toggle && menu) {
            const dropdown = menu.querySelector(".cy-dropdown");
            const servicesButton = dropdown?.querySelector(".cy-dropdown__toggle");

            const setServicesOpen = open => {
                if (!dropdown || !servicesButton) return;
                dropdown.classList.toggle("is-open", open);
                servicesButton.setAttribute("aria-expanded", open ? "true" : "false");
            };

            const setOpen = open => {
                menu.classList.toggle("open", open);
                toggle.setAttribute("aria-expanded", open ? "true" : "false");
                document.body.classList.toggle("menu-open", open);
                if (!open) setServicesOpen(false);
            };

            toggle.addEventListener("click", () => setOpen(!menu.classList.contains("open")));
            servicesButton?.addEventListener("click", () => {
                setServicesOpen(!dropdown.classList.contains("is-open"));
            });

            // Mouse users can hover over Services; touch and keyboard users click it.
            dropdown?.addEventListener("pointerenter", event => {
                if (event.pointerType === "mouse" && window.innerWidth > 1240) setServicesOpen(true);
            });
            dropdown?.addEventListener("pointerleave", event => {
                if (event.pointerType === "mouse" && window.innerWidth > 1240) setServicesOpen(false);
            });
            dropdown?.addEventListener("focusout", event => {
                if (!dropdown.contains(event.relatedTarget)) setServicesOpen(false);
            });

            menu.querySelectorAll("a").forEach(link =>
                link.addEventListener("click", () => setOpen(false))
            );

            document.addEventListener("keydown", event => {
                if (event.key !== "Escape") return;
                if (dropdown?.classList.contains("is-open")) {
                    setServicesOpen(false);
                    servicesButton?.focus();
                } else if (menu.classList.contains("open")) {
                    setOpen(false);
                    toggle.focus();
                }
            });
            document.addEventListener("click", event => {
                if (dropdown && !dropdown.contains(event.target)) setServicesOpen(false);
                if (!menu.contains(event.target) && !toggle.contains(event.target)) setOpen(false);
            });
            window.addEventListener("resize", () => {
                if (window.innerWidth > 1240) setOpen(false);
            });

            // Anchor sections are on the existing IT homepage until corporate pages are built.
            const updateCorporateActive = () => {
                const path = window.location.pathname.toLowerCase();
                if (path !== "/" && path !== "/home/index") return;
                const hash = window.location.hash.toLowerCase();
                const selected = hash === "#about" ? "about" : hash === "#contact" ? "contact" : "home";
                menu.querySelectorAll("[data-primary-nav]").forEach(link => {
                    if (link.dataset.primaryNav !== "home" &&
                        link.dataset.primaryNav !== "about" &&
                        link.dataset.primaryNav !== "contact") return;
                    const isSelected = link.dataset.primaryNav === selected;
                    link.classList.toggle("active", isSelected);
                    if (isSelected) link.setAttribute("aria-current", "page");
                    else link.removeAttribute("aria-current");
                });
            };
            window.addEventListener("hashchange", updateCorporateActive);
            updateCorporateActive();
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
