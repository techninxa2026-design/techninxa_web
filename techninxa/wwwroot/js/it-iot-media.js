/* IT & IoT media-only progressive enhancement. No DB or form logic changes. */
(() => {
    "use strict";

    const selector = "img[data-it-preview], img[data-it-avatar]";
    function recover(img) {
        if (!(img instanceof HTMLImageElement) || !img.matches(selector)) return;
        if (img.dataset.itFallbackUsed === "true") {
            img.classList.add("it-media-image-error");
            const card = img.closest(".home-project-visual, .library-image, .home-team-portrait");
            card?.classList.add("it-media-failed");
            return;
        }
        const fallback = img.dataset.itFallback;
        if (!fallback || fallback === img.getAttribute("src")) {
            img.dataset.itFallbackUsed = "true";
            img.classList.add("it-media-image-error");
            return;
        }
        img.dataset.itFallbackUsed = "true";
        img.src = fallback;
        img.classList.add("it-media-fallback-active");
        img.alt = img.hasAttribute("data-it-avatar")
            ? "Illustrated team member avatar"
            : "Illustrative project interface preview";
    }

    // Error events on HTMLImageElement do not bubble; capture makes this work
    // for delayed images, failed uploaded URLs and existing local assets.
    document.addEventListener("error", ev => recover(ev.target), true);

    function inspect() {
        document.querySelectorAll(selector).forEach(img => {
            if (img.complete && img.naturalWidth === 0) recover(img);
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", inspect, { once: true });
    } else {
        inspect();
    }
})();