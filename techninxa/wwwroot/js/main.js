
(() => {
    'use strict';

    const reducedMotion = window.matchMedia(
        '(prefers-reduced-motion: reduce)'
    ).matches;

    const finePointer = window.matchMedia(
        '(hover: hover) and (pointer: fine)'
    ).matches;

    const gsap = window.gsap || null;
    const ScrollTrigger = window.ScrollTrigger || null;
    const Flip = window.Flip || null;
    const SplitText = window.SplitText || null;

    const canAnimate = Boolean(gsap && !reducedMotion);

    if (gsap) {
        const plugins = [ScrollTrigger, Flip, SplitText].filter(Boolean);
        if (plugins.length) gsap.registerPlugin(...plugins);
    }

    const normalize = value =>
        (value || '').toString().trim()
            .replace(/\s+/g, ' ')
            .toLocaleLowerCase();

    const cleanLabel = value =>
        (value || '').toString().trim().replace(/\s+/g, ' ') || 'Other';

    function markVisible(elements) {
        elements.forEach(element =>
            element && element.classList.add('is-visible')
        );
    }

    /* =========================================================
       CUSTOM CURSOR
       ========================================================= */

    function setupCustomCursor() {
        if (!finePointer) return;

        // Prevent duplicate cursors if this script is accidentally loaded twice.
        if (document.querySelector('.custom-cursor')) return;

        const cursor = document.createElement('div');
        cursor.className = 'custom-cursor';
        cursor.setAttribute('aria-hidden', 'true');

        const dot = document.createElement('span');
        dot.className = 'custom-cursor-dot';

        cursor.appendChild(dot);
        document.body.appendChild(cursor);

        let mouseX = -100;
        let mouseY = -100;
        let cursorX = -100;
        let cursorY = -100;
        let frame = null;
        let active = false;

        function renderCursor() {
            const easing = reducedMotion ? 1 : 0.20;

            cursorX += (mouseX - cursorX) * easing;
            cursorY += (mouseY - cursorY) * easing;

            cursor.style.transform =
                `translate3d(${cursorX}px, ${cursorY}px, 0)`;

            if (
                Math.abs(mouseX - cursorX) > 0.15 ||
                Math.abs(mouseY - cursorY) > 0.15
            ) {
                frame = requestAnimationFrame(renderCursor);
            } else {
                frame = null;
            }
        }

        function moveCursor(event) {
            if (event.pointerType && event.pointerType !== 'mouse') return;

            mouseX = event.clientX;
            mouseY = event.clientY;

            if (!active) {
                active = true;
                cursor.classList.add('is-active');
            }

            if (frame === null) {
                frame = requestAnimationFrame(renderCursor);
            }
        }

        function updateHover(event) {
            if (!(event.target instanceof Element)) return;

            const interactive = event.target.closest(
                'a, button, input, textarea, select, label, ' +
                '[role="button"], [data-cursor="hover"]'
            );

            cursor.classList.toggle('is-hovering', Boolean(interactive));
        }

        document.addEventListener('pointermove', moveCursor, {
            passive: true
        });

        document.addEventListener('pointerover', updateHover);

        document.addEventListener('pointerdown', () => {
            cursor.classList.add('is-clicking');
        });

        document.addEventListener('pointerup', () => {
            cursor.classList.remove('is-clicking');
        });

        document.addEventListener('pointercancel', () => {
            cursor.classList.remove('is-clicking');
        });

        document.addEventListener('pointerout', event => {
            if (!event.relatedTarget) {
                active = false;
                cursor.classList.remove('is-active', 'is-hovering');
            }
        });

        window.addEventListener('blur', () => {
            active = false;
            cursor.classList.remove(
                'is-active',
                'is-hovering',
                'is-clicking'
            );
        });

        window.addEventListener('focus', () => {
            active = false;
        });
    }

    /* =========================================================
       BUTTON FEEDBACK AND FILTERS
       ========================================================= */

    function animateControlFeedback(button) {
        if (!canAnimate || !button) return;

        gsap.fromTo(
            button,
            { scale: 0.96 },
            {
                scale: 1,
                duration: 0.42,
                ease: 'back.out(2)',
                overwrite: 'auto'
            }
        );
    }

    function createFilterButton(
        label,
        count,
        value,
        onSelect,
        selected = false
    ) {
        const button = document.createElement('button');

        button.type = 'button';
        button.className = 'home-filter-chip';
        button.dataset.filterValue = value;
        button.setAttribute('aria-pressed', selected ? 'true' : 'false');

        const text = document.createElement('span');
        text.textContent = label;
        button.appendChild(text);

        const number = document.createElement('span');
        number.className = 'home-filter-chip-count';
        number.textContent = String(count);
        number.setAttribute('aria-hidden', 'true');
        button.appendChild(number);

        button.addEventListener('click', () => {
            animateControlFeedback(button);
            onSelect(value);
        });

        return button;
    }

    function setSelectedFilter(container, value) {
        container.querySelectorAll('[data-filter-value]').forEach(button => {
            const selected = button.dataset.filterValue === value;

            button.setAttribute(
                'aria-pressed',
                selected ? 'true' : 'false'
            );

            if (canAnimate && selected) {
                gsap.fromTo(
                    button,
                    { boxShadow: '0 0 0 0 rgba( 255, 75, 11,0)' },
                    {
                        boxShadow: '0 0 0 5px rgba( 255, 75, 11,0.10)',
                        duration: 0.24,
                        yoyo: true,
                        repeat: 1,
                        ease: 'power2.out',
                        overwrite: 'auto'
                    }
                );
            }
        });
    }

    function animateCounter(element, value, noun) {
        if (!element) return;

        element.textContent =
            `${value} ${value === 1 ? noun : `${noun}s`}`;

        if (canAnimate) {
            gsap.fromTo(
                element,
                { scale: 0.95, y: 3, autoAlpha: 0.72 },
                {
                    scale: 1,
                    y: 0,
                    autoAlpha: 1,
                    duration: 0.42,
                    ease: 'back.out(1.7)',
                    overwrite: 'auto'
                }
            );
        }
    }

    function clearCardTweens(cards) {
        if (!gsap) return;

        gsap.killTweensOf(cards);
        gsap.set(cards, {
            clearProps: 'transform,opacity,visibility'
        });
    }

    function animateEmptyState(element, show) {
        if (!element) return;

        if (!canAnimate) {
            element.hidden = !show;
            return;
        }

        gsap.killTweensOf(element);

        if (show) {
            element.hidden = false;

            gsap.fromTo(
                element,
                { autoAlpha: 0, y: 12, scale: 0.99 },
                {
                    autoAlpha: 1,
                    y: 0,
                    scale: 1,
                    duration: 0.42,
                    ease: 'power3.out',
                    overwrite: 'auto'
                }
            );
        } else if (!element.hidden) {
            gsap.to(element, {
                autoAlpha: 0,
                y: -8,
                duration: 0.16,
                ease: 'power2.in',
                overwrite: 'auto',
                onComplete: () => {
                    element.hidden = true;
                    gsap.set(element, {
                        clearProps: 'opacity,visibility,transform'
                    });
                }
            });
        } else {
            element.hidden = true;
        }
    }

    function refreshScrollPositions() {
        if (!canAnimate || !ScrollTrigger) return;

        requestAnimationFrame(() => ScrollTrigger.refresh());
    }

    /* =========================================================
       ANIMATED PROJECT / TEAM GRID FILTERING
       ========================================================= */

    function applyAnimatedGridFilter({
        cards,
        visibleCards,
        grid,
        featuredClass,
        animate
    }) {
        const visibleSet = new Set(visibleCards);

        const currentlyVisible = cards.filter(card => !card.hidden);
        const leaving = currentlyVisible.filter(
            card => !visibleSet.has(card)
        );
        const entering = visibleCards.filter(card => card.hidden);

        const sortByOriginal = (a, b) =>
            Number(a.dataset.originalOrder) -
            Number(b.dataset.originalOrder);

        const commit = () => {
            const staying = cards.filter(
                card => !card.hidden && visibleSet.has(card)
            );

            if (canAnimate) {
                gsap.set(cards, {
                    clearProps: 'transform,opacity,visibility'
                });
                gsap.set(staying, { autoAlpha: 1 });
            }

            const state =
                canAnimate && Flip && animate && staying.length
                    ? Flip.getState(staying)
                    : null;

            cards.forEach(card => {
                card.hidden = !visibleSet.has(card);

                if (featuredClass) {
                    card.classList.remove(featuredClass);
                }

                if (visibleSet.has(card)) {
                    card.classList.add('is-visible');
                }
            });

            visibleCards.forEach(card => grid.appendChild(card));

            cards
                .filter(card => !visibleSet.has(card))
                .sort(sortByOriginal)
                .forEach(card => grid.appendChild(card));

            if (featuredClass && visibleCards[0]) {
                visibleCards[0].classList.add(featuredClass);
            }

            if (!canAnimate || !animate) {
                if (canAnimate) {
                    gsap.set(visibleCards, {
                        autoAlpha: 1,
                        clearProps: 'transform'
                    });
                }

                refreshScrollPositions();
                return;
            }

            if (state && Flip) {
                Flip.from(state, {
                    duration: 0.68,
                    ease: 'power3.inOut',
                    absolute: false,
                    scale: true,
                    fade: true,
                    stagger: { each: 0.035, from: 'start' },
                    onComplete: refreshScrollPositions
                });
            }

            if (entering.length) {
                gsap.set(entering, {
                    autoAlpha: 0,
                    y: 26,
                    scale: 0.975
                });

                gsap.to(entering, {
                    autoAlpha: 1,
                    y: 0,
                    scale: 1,
                    duration: 0.58,
                    ease: 'power3.out',
                    stagger: 0.055,
                    overwrite: 'auto',
                    onComplete: () => {
                        gsap.set(entering, {
                            clearProps: 'transform'
                        });
                    }
                });
            }

            if (!state && visibleCards.length && entering.length === 0) {
                gsap.fromTo(
                    visibleCards,
                    { autoAlpha: 0.6, y: 13 },
                    {
                        autoAlpha: 1,
                        y: 0,
                        duration: 0.42,
                        stagger: 0.035,
                        ease: 'power3.out',
                        overwrite: 'auto',
                        onComplete: refreshScrollPositions
                    }
                );
            } else {
                refreshScrollPositions();
            }
        };

        if (!canAnimate || !animate) {
            cards.forEach(card => {
                card.hidden = !visibleSet.has(card);

                if (featuredClass) {
                    card.classList.remove(featuredClass);
                }

                if (visibleSet.has(card)) {
                    card.classList.add('is-visible');
                }
            });

            visibleCards.forEach(card => grid.appendChild(card));

            cards
                .filter(card => !visibleSet.has(card))
                .sort(sortByOriginal)
                .forEach(card => grid.appendChild(card));

            if (featuredClass && visibleCards[0]) {
                visibleCards[0].classList.add(featuredClass);
            }

            if (canAnimate) {
                gsap.set(visibleCards, {
                    autoAlpha: 1,
                    clearProps: 'transform'
                });
            }

            refreshScrollPositions();
            return;
        }

        clearCardTweens(cards);

        if (leaving.length) {
            gsap.to(leaving, {
                autoAlpha: 0,
                y: -10,
                scale: 0.985,
                duration: 0.18,
                stagger: 0.012,
                ease: 'power2.in',
                overwrite: 'auto',
                onComplete: commit
            });
        } else {
            commit();
        }
    }

    /* =========================================================
       PROJECT / TEAM CARD MOTION
       ========================================================= */

    function addCardSpotlight(card, imageSelector, openSelector) {
        const image = imageSelector
            ? card.querySelector(imageSelector)
            : null;

        const open = openSelector
            ? card.querySelector(openSelector)
            : null;

        const setSpotX = gsap
            ? gsap.quickSetter(card, '--spot-x', '%')
            : null;

        const setSpotY = gsap
            ? gsap.quickSetter(card, '--spot-y', '%')
            : null;

        const tiltX =
            canAnimate && finePointer
                ? gsap.quickTo(card, 'rotationX', {
                    duration: 0.34,
                    ease: 'power3.out'
                })
                : null;

        const tiltY =
            canAnimate && finePointer
                ? gsap.quickTo(card, 'rotationY', {
                    duration: 0.34,
                    ease: 'power3.out'
                })
                : null;

        if (!canAnimate) return;

        card.addEventListener('pointermove', event => {
            if (!finePointer || event.pointerType !== 'mouse') return;

            const rect = card.getBoundingClientRect();
            if (!rect.width || !rect.height) return;

            const x = (event.clientX - rect.left) / rect.width;
            const y = (event.clientY - rect.top) / rect.height;

            if (setSpotX) setSpotX(x * 100);
            if (setSpotY) setSpotY(y * 100);

            if (tiltX) tiltX((0.5 - y) * 4.5);
            if (tiltY) tiltY((x - 0.5) * 5.5);
        });

        card.addEventListener('pointerenter', () => {
            if (image) {
                gsap.to(image, {
                    scale: 1.055,
                    duration: 0.72,
                    ease: 'power3.out',
                    overwrite: 'auto'
                });
            }

            gsap.to(card, {
                transformPerspective: 1100,
                y: -5,
                duration: 0.32,
                ease: 'power3.out',
                overwrite: 'auto'
            });

            if (open) {
                gsap.to(open, {
                    rotation: 45,
                    scale: 1.06,
                    duration: 0.3,
                    ease: 'back.out(1.7)',
                    overwrite: 'auto'
                });
            }

            card.classList.add('is-tilting');
        });

        card.addEventListener('pointerleave', () => {
            if (image) {
                gsap.to(image, {
                    scale: 1,
                    duration: 0.68,
                    ease: 'power3.out',
                    overwrite: 'auto'
                });
            }

            gsap.to(card, {
                rotationX: 0,
                rotationY: 0,
                y: 0,
                duration: 0.46,
                ease: 'power3.out',
                overwrite: 'auto'
            });

            if (open) {
                gsap.to(open, {
                    rotation: 0,
                    scale: 1,
                    duration: 0.28,
                    ease: 'power3.out',
                    overwrite: 'auto'
                });
            }

            card.classList.remove('is-tilting');
        });
    }

    function addParallax(image, trigger) {
        if (!canAnimate || !ScrollTrigger || !image) return;

        gsap.to(image, {
            yPercent: 5,
            ease: 'none',
            scrollTrigger: {
                trigger,
                start: 'top bottom',
                end: 'bottom top',
                scrub: 0.7,
                invalidateOnRefresh: true
            }
        });
    }

    function addMagneticEffect(element) {
        if (
            !canAnimate ||
            !finePointer ||
            element.dataset.magneticReady === 'true'
        ) {
            return;
        }

        element.dataset.magneticReady = 'true';

        const moveX = gsap.quickTo(element, 'x', {
            duration: 0.36,
            ease: 'power3.out'
        });

        const moveY = gsap.quickTo(element, 'y', {
            duration: 0.36,
            ease: 'power3.out'
        });

        element.addEventListener('pointermove', event => {
            if (event.pointerType !== 'mouse') return;

            const rect = element.getBoundingClientRect();

            moveX(
                ((event.clientX - rect.left) / rect.width - 0.5) * 9
            );

            moveY(
                ((event.clientY - rect.top) / rect.height - 0.5) * 7
            );
        });

        element.addEventListener('pointerleave', () => {
            moveX(0);
            moveY(0);
        });
    }

    /* =========================================================
       PROJECT FILTERING, SEARCH AND SORT
       ========================================================= */

    function setupProjects() {
        const section = document.querySelector('.home-projects');
        if (!section) return;

        const controls = section.querySelector('[data-project-controls]');
        const grid = section.querySelector('[data-project-grid]');
        const filters = section.querySelector('[data-project-filters]');
        const search = section.querySelector('[data-project-search]');
        const sort = section.querySelector('[data-project-sort]');
        const count = section.querySelector('[data-project-count]');
        const status = section.querySelector('[data-project-status]');
        const empty = section.querySelector('[data-project-empty]');
        const reset = section.querySelector('[data-project-reset]');

        const cards = Array.from(
            section.querySelectorAll('[data-project-card]')
        );

        if (
            !controls ||
            !grid ||
            !filters ||
            !search ||
            !sort ||
            cards.length === 0
        ) {
            return;
        }

        cards.forEach((card, index) => {
            card.dataset.originalOrder =
                card.dataset.order || String(index + 1);

            card.dataset.normalizedCategory =
                normalize(cleanLabel(card.dataset.category));

            card.dataset.searchText = normalize([
                card.dataset.title,
                card.dataset.category,
                card.dataset.description,
                card.textContent
            ].join(' '));

            markVisible([card]);

            addCardSpotlight(
                card,
                '.home-project-visual > img',
                '.home-project-open'
            );

            addParallax(
                card.querySelector('.home-project-visual > img'),
                card
            );
        });

        const categoryMap = new Map();

        cards.forEach(card => {
            const name = cleanLabel(card.dataset.category);
            const key = normalize(name);

            if (!categoryMap.has(key)) {
                categoryMap.set(key, { name, count: 0 });
            }

            categoryMap.get(key).count += 1;
        });

        const categories = Array.from(categoryMap.entries())
            .sort((a, b) => a[1].name.localeCompare(b[1].name));

        let activeCategory = 'all';

        function selectCategory(value) {
            activeCategory = value;
            setSelectedFilter(filters, value);
            updateProjects(true);
        }

        filters.replaceChildren();

        filters.appendChild(
            createFilterButton(
                'All work',
                cards.length,
                'all',
                selectCategory,
                true
            )
        );

        categories.forEach(([key, item]) => {
            filters.appendChild(
                createFilterButton(
                    item.name,
                    item.count,
                    key,
                    selectCategory
                )
            );
        });

        function updateProjects(animate) {
            const query = normalize(search.value);
            const sortMode = sort.value;

            const visible = cards.filter(card => {
                const categoryMatches =
                    activeCategory === 'all' ||
                    card.dataset.normalizedCategory === activeCategory;

                const searchMatches =
                    !query || card.dataset.searchText.includes(query);

                return categoryMatches && searchMatches;
            });

            visible.sort((a, b) => {
                if (sortMode === 'az') {
                    return cleanLabel(a.dataset.title)
                        .localeCompare(cleanLabel(b.dataset.title));
                }

                if (sortMode === 'za') {
                    return cleanLabel(b.dataset.title)
                        .localeCompare(cleanLabel(a.dataset.title));
                }

                return Number(a.dataset.originalOrder) -
                    Number(b.dataset.originalOrder);
            });

            animateCounter(count, visible.length, 'project');

            if (status) {
                const categoryLabel = activeCategory === 'all'
                    ? 'All categories'
                    : (
                        categories.find(([key]) => key === activeCategory)
                            ?.[1]?.name || 'Selected category'
                    );

                status.textContent =
                    query || activeCategory !== 'all'
                        ? `Showing ${visible.length} of ${cards.length} projects. ` +
                          `${categoryLabel}` +
                          (query
                              ? ` · Search: “${search.value.trim()}”`
                              : '')
                        : `Showing all ${cards.length} projects.`;
            }

            applyAnimatedGridFilter({
                cards,
                visibleCards: visible,
                grid,
                featuredClass: 'home-project-card-featured',
                animate
            });

            animateEmptyState(empty, visible.length === 0);
        }

        search.addEventListener('input', () => updateProjects(true));
        sort.addEventListener('change', () => updateProjects(true));

        reset?.addEventListener('click', () => {
            activeCategory = 'all';
            search.value = '';
            sort.value = 'featured';

            setSelectedFilter(filters, 'all');
            updateProjects(true);
            search.focus();
        });

        controls.hidden = false;
        updateProjects(false);
    }

    /* =========================================================
       TEAM FILTERING AND SEARCH
       ========================================================= */

    function setupTeam() {
        const section = document.querySelector('.home-team-section');
        if (!section) return;

        const controls = section.querySelector('[data-team-controls]');
        const filters = section.querySelector('[data-team-filters]');
        const search = section.querySelector('[data-team-search]');
        const count = section.querySelector('[data-team-count]');
        const status = section.querySelector('[data-team-status]');
        const empty = section.querySelector('[data-team-empty]');
        const reset = section.querySelector('[data-team-reset]');
        const grid = section.querySelector('[data-team-grid]');

        const cards = Array.from(
            section.querySelectorAll('[data-team-card]')
        );

        if (
            !controls ||
            !filters ||
            !search ||
            !grid ||
            cards.length === 0
        ) {
            return;
        }

        cards.forEach((card, index) => {
            card.dataset.originalOrder =
                card.dataset.order || String(index + 1);

            card.dataset.normalizedDepartment =
                normalize(cleanLabel(card.dataset.department));

            card.dataset.searchText = normalize([
                card.dataset.name,
                card.dataset.role,
                card.dataset.department,
                card.textContent
            ].join(' '));

            markVisible([card]);

            addCardSpotlight(
                card,
                '.home-team-portrait > img',
                null
            );

            addParallax(
                card.querySelector('.home-team-portrait > img'),
                card
            );
        });

        const departmentMap = new Map();

        cards.forEach(card => {
            const name = cleanLabel(card.dataset.department);
            const key = normalize(name);

            if (!departmentMap.has(key)) {
                departmentMap.set(key, { name, count: 0 });
            }

            departmentMap.get(key).count += 1;
        });

        const departments = Array.from(departmentMap.entries())
            .sort((a, b) => a[1].name.localeCompare(b[1].name));

        let activeDepartment = 'all';

        function selectDepartment(value) {
            activeDepartment = value;
            setSelectedFilter(filters, value);
            updateTeam(true);
        }

        filters.replaceChildren();

        filters.appendChild(
            createFilterButton(
                'Everyone',
                cards.length,
                'all',
                selectDepartment,
                true
            )
        );

        departments.forEach(([key, item]) => {
            filters.appendChild(
                createFilterButton(
                    item.name,
                    item.count,
                    key,
                    selectDepartment
                )
            );
        });

        function updateTeam(animate) {
            const query = normalize(search.value);

            const visible = cards.filter(card => {
                const departmentMatches =
                    activeDepartment === 'all' ||
                    card.dataset.normalizedDepartment === activeDepartment;

                const searchMatches =
                    !query || card.dataset.searchText.includes(query);

                return departmentMatches && searchMatches;
            });

            animateCounter(count, visible.length, 'team member');

            if (status) {
                status.textContent =
                    query || activeDepartment !== 'all'
                        ? `Showing ${visible.length} of ${cards.length} team members.`
                        : `Meet all ${cards.length} team members.`;
            }

            applyAnimatedGridFilter({
                cards,
                visibleCards: visible,
                grid,
                featuredClass: null,
                animate
            });

            animateEmptyState(empty, visible.length === 0);
        }

        search.addEventListener('input', () => updateTeam(true));

        reset?.addEventListener('click', () => {
            activeDepartment = 'all';
            search.value = '';

            setSelectedFilter(filters, 'all');
            updateTeam(true);
            search.focus();
        });

        controls.hidden = false;
        updateTeam(false);
    }

    /* =========================================================
       SCROLL REVEALS AND HEADING ANIMATIONS
       ========================================================= */

    function scrollReveal(element, options = {}) {
        if (!element) return;

        element.classList.add('is-visible');

        if (!canAnimate || !ScrollTrigger) return;

        gsap.fromTo(
            element,
            {
                autoAlpha: 0,
                y: options.y ?? 34,
                filter: options.blur ? 'blur(7px)' : 'blur(0px)'
            },
            {
                autoAlpha: 1,
                y: 0,
                filter: 'blur(0px)',
                duration: options.duration ?? 0.88,
                ease: options.ease ?? 'power3.out',
                delay: options.delay ?? 0,
                scrollTrigger: {
                    trigger: element,
                    start: options.start ?? 'top 84%',
                    once: true
                }
            }
        );
    }

    function animateSplitHeading(heading) {
        if (!heading) return;

        heading.classList.add('is-visible');

        if (!canAnimate || !ScrollTrigger) return;

        if (SplitText && typeof SplitText.create === 'function') {
            try {
                SplitText.create(heading, {
                    type: 'words',
                    mask: 'words',
                    wordsClass: 'home-gsap-word',

                    onSplit: split => {
                        const timeline = gsap.timeline({
                            scrollTrigger: {
                                trigger: heading,
                                start: 'top 83%',
                                once: true
                            }
                        });

                        timeline.fromTo(
                            split.words,
                            {
                                yPercent: 115,
                                autoAlpha: 0,
                                rotateX: -18
                            },
                            {
                                yPercent: 0,
                                autoAlpha: 1,
                                rotateX: 0,
                                duration: 0.82,
                                stagger: 0.045,
                                ease: 'power4.out'
                            }
                        );

                        return timeline;
                    }
                });

                return;
            } catch (error) {
                // Fall back to the standard GSAP reveal.
            }
        }

        scrollReveal(heading, {
            y: 28,
            duration: 0.8,
            start: 'top 83%'
        });
    }

    function setupSectionAnimations() {
        const projectSection = document.querySelector('.home-projects');
        const teamSection = document.querySelector('.home-team-section');

        if (!projectSection && !teamSection) return;

        if (canAnimate) {
            document.body.classList.add('gsap-home-animations');
        }

        if (projectSection) {
            const head =
                projectSection.querySelector('.home-projects-head');

            const controls =
                projectSection.querySelector('.home-project-controls');

            const cards = Array.from(
                projectSection.querySelectorAll('[data-project-card]')
            );

            const cta =
                projectSection.querySelector('.home-project-cta');

            const links = Array.from(
                projectSection.querySelectorAll(
                    '.home-project-cta a, .home-projects-link'
                )
            );

            scrollReveal(head, {
                y: 38,
                duration: 0.92,
                blur: true
            });

            animateSplitHeading(
                projectSection.querySelector('.home-projects-title')
            );

            scrollReveal(controls, {
                y: 20,
                duration: 0.68,
                delay: 0.04,
                start: 'top 88%'
            });

            markVisible(cards);

            if (canAnimate && ScrollTrigger && cards.length) {
                gsap.fromTo(
                    cards,
                    { autoAlpha: 0, y: 48, scale: 0.965 },
                    {
                        autoAlpha: 1,
                        y: 0,
                        scale: 1,
                        duration: 0.86,
                        stagger: { each: 0.085, from: 'start' },
                        ease: 'power3.out',
                        scrollTrigger: {
                            trigger: projectSection.querySelector(
                                '[data-project-grid]'
                            ),
                            start: 'top 84%',
                            once: true
                        },
                        onComplete: () => {
                            gsap.set(cards, {
                                clearProps: 'transform'
                            });
                        }
                    }
                );
            }

            scrollReveal(cta, {
                y: 28,
                duration: 0.86,
                blur: true,
                start: 'top 86%'
            });

            links.forEach(addMagneticEffect);
        }

        if (teamSection) {
            const heading =
                teamSection.querySelector('.home-team-heading');

            const controls =
                teamSection.querySelector('.home-team-controls');

            const cards = Array.from(
                teamSection.querySelectorAll('[data-team-card]')
            );

            const cta =
                teamSection.querySelector('.home-team-cta');

            const title =
                teamSection.querySelector('.home-team-title');

            const links = Array.from(
                teamSection.querySelectorAll(
                    '.home-team-card a, .home-team-cta a'
                )
            );

            scrollReveal(heading, {
                y: 36,
                duration: 0.92,
                blur: true
            });

            animateSplitHeading(title);

            scrollReveal(controls, {
                y: 20,
                duration: 0.68,
                delay: 0.04,
                start: 'top 88%'
            });

            markVisible(cards);

            if (canAnimate && ScrollTrigger && cards.length) {
                gsap.fromTo(
                    cards,
                    {
                        autoAlpha: 0,
                        y: 42,
                        scale: 0.97,
                        rotateX: 2
                    },
                    {
                        autoAlpha: 1,
                        y: 0,
                        scale: 1,
                        rotateX: 0,
                        duration: 0.82,
                        stagger: { each: 0.075, from: 'start' },
                        ease: 'power3.out',
                        scrollTrigger: {
                            trigger: teamSection.querySelector(
                                '[data-team-grid]'
                            ),
                            start: 'top 84%',
                            once: true
                        },
                        onComplete: () => {
                            gsap.set(cards, {
                                clearProps: 'transform'
                            });
                        }
                    }
                );
            }

            scrollReveal(cta, {
                y: 24,
                duration: 0.8,
                start: 'top 88%'
            });

            links.forEach(addMagneticEffect);
        }

        if (canAnimate && ScrollTrigger) {
            window.addEventListener(
                'load',
                () => ScrollTrigger.refresh(),
                { once: true }
            );

            ScrollTrigger.refresh();
        } else {
            document.querySelectorAll(
                '.home-projects .reveal, .home-team-section .reveal'
            ).forEach(element => {
                element.classList.add('is-visible');
            });
        }
    }

    /* =========================================================
       CONTACT LINKS
       ========================================================= */

    function setupContactLinks() {
        document.querySelectorAll(
            '.home-project-cta a[href="#contact"], ' +
            '.home-team-cta a[href="#contact"]'
        ).forEach(link => {
            link.addEventListener('click', event => {
                const target = document.querySelector('#contact');
                if (!target) return;

                event.preventDefault();

                target.scrollIntoView({
                    behavior: reducedMotion ? 'auto' : 'smooth',
                    block: 'start'
                });

                if (window.history && window.history.replaceState) {
                    window.history.replaceState(null, '', '#contact');
                }
            });
        });
    }

    /* =========================================================
       INITIALIZATION
       ========================================================= */

    function initialize() {
        // Cursor is independent of whether project/team sections exist.
        setupCustomCursor();

        setupProjects();
        setupTeam();
        setupContactLinks();
        setupSectionAnimations();
    }

    if (document.readyState === 'loading') {
        document.addEventListener(
            'DOMContentLoaded',
            initialize,
            { once: true }
        );
    } else {
        initialize();
    }
})();
