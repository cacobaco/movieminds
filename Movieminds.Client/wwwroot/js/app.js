window.blazorInterop = {
    hideModal: function (selector) {
        $(selector).modal('hide');
    },
    subscribeElementScrollEnd: function (selector, functionName, dotnetReference) {
        const element = document.querySelector(selector);

        element.addEventListener('scroll', function () {
            const isEnd = element.scrollHeight - element.clientHeight <= element.scrollTop + 1;

            if (isEnd) {
                dotnetReference.invokeMethodAsync(functionName);
            }
        });
    },
};
