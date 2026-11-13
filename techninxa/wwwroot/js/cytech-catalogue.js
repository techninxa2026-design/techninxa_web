/* Client-side product catalogue filters. Catalogue data remains server-rendered and crawlable. */
(() => {
    "use strict";

    function ready() {
        const search = document.getElementById("cyCatalogueSearch");
        const count = document.getElementById("cyCatalogueCount");
        const empty = document.getElementById("cyCatalogueEmpty");
        const reset = document.getElementById("cyCatalogueReset");
        const cards = [...document.querySelectorAll("[data-cy-product-card]")];
        const filters = [...document.querySelectorAll("[data-cy-filter]")];

        if (!search || !cards.length) return;

        const normalize = value => String(value || "").toLocaleLowerCase().trim();
        const initialFamily = new URLSearchParams(window.location.search).get("family");
        let family = filters.some(button => button.dataset.cyFilter === initialFamily) ? initialFamily : "all";

        const apply = () => {
            const query = normalize(search.value);
            let visible = 0;
            cards.forEach(card => {
                const familyMatch = family === "all" || card.dataset.family === family;
                const textMatch = !query || normalize(card.dataset.search).includes(query) || normalize(card.dataset.family).includes(query);
                const show = familyMatch && textMatch;
                card.hidden = !show;
                // The grid sets display explicitly for visible cards, so ensure the HTML hidden attribute wins.
                card.style.display = show ? "" : "none";
                if (show) visible++;
            });

            filters.forEach(button => {
                const isSelected = button.dataset.cyFilter === family;
                button.classList.toggle("is-active", isSelected);
                button.setAttribute("aria-pressed", isSelected ? "true" : "false");
            });
            if (count) count.textContent = visible === 1 ? "1 section" : visible + " sections";
            empty?.classList.toggle("is-visible", visible === 0);
        };

        const selectFamily = value => {
            family = value;
            const url = new URL(window.location.href);
            if (value === "all") url.searchParams.delete("family");
            else url.searchParams.set("family", value);
            window.history.replaceState(null, "", url.pathname + url.search + url.hash);
            apply();
        };

        filters.forEach(button => button.addEventListener("click", () => selectFamily(button.dataset.cyFilter || "all")));
        search.addEventListener("input", apply);
        reset?.addEventListener("click", () => {
            search.value = "";
            selectFamily("all");
            search.focus();
        });
        apply();
    }

    if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", ready, { once: true });
    else ready();
})();