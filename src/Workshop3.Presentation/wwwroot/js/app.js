const handlerCallbacks = {}

window.registerKeyboardEventHandler = (handler) => {
    const callback = evt => {
        handler.invokeMethodAsync('OnKeyDownAsync', {
            key: evt.key
        })
    }
    
    handlerCallbacks[handler] = callback

    document.addEventListener('keydown', callback)
}

window.unregisterKeyboardEventHandler = (handler) => {
    const callback = handlerCallbacks[handler]
    
    if (callback) {
        document.removeEventListener('keydown', callback)
    }
}