class Observable {
    constructor() {
        this._handlers = []
    }

    subscribe(event, handler) {
        this._handlers[event] = this._handlers[event] || []
        this._handlers[event].push(handler)
    }

    publish(event, eventData) {
        const eventHandlers = this._handlers[event]

        if (eventHandlers) {
            for (var i = 0, l = eventHandlers.length; i < l; ++i) {
                eventHandlers[i].call({}, eventData)
            }
        }
    }
}

const observableInstance = new Observable();

export default observableInstance;