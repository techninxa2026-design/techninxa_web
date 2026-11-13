/* Resilient image loading for catalogue-based equipment photography.
   Applies on all corporate pages, including product details and division pages. */
(() => {
    "use strict";

    const selector = "img[data-cy-catalogue-photo]";

    function recover(image) {
        if (!(image instanceof HTMLImageElement) || !image.matches(selector)) return;

        if (image.dataset.imgRecovered !== "true") {
            image.dataset.imgRecovered = "true";
            const fallback = image.dataset.fallbackSrc;
            if (fallback && fallback !== image.currentSrc && fallback !== image.src) {
                image.src = fallback;
                return;
            }
        }

        image.dataset.imgFailed = "true";
        const frame = image.closest(
            ".cy-prod-photo-frame, .cy-prod-detail-photo, .cy-pro-feature-photo, .cy-pro-family-image"
        );
        if (frame) frame.classList.add("is-unavailable");
    }

    // Use capture: native image "error" events do not bubble.
    document.addEventListener("error", event => {
        if (event.target instanceof HTMLImageElement) recover(event.target);
    }, true);

    function inspectImages() {
        document.querySelectorAll(selector).forEach(image => {
            if (image.complete && image.naturalWidth === 0) recover(image);
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", inspectImages, { once: true });
    } else {
        inspectImages();
    }
})();