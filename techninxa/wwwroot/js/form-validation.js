(() => {
    for (const input of document.querySelectorAll('input[data-val="true"], textarea[data-val="true"], select[data-val="true"]')) {
        if (input.hasAttribute('data-val-required') && input.type !== 'checkbox' && input.type !== 'hidden') input.required = true;
        const max = input.getAttribute('data-val-length-max') || input.getAttribute('data-val-maxlength-max');
        if (max && (input instanceof HTMLTextAreaElement || ['text','email','password','search','tel','url'].includes(input.type))) input.maxLength = Number(max);
        if (input.hasAttribute('data-val-email') && input.type === 'text') input.type = 'email';
    }
})();
