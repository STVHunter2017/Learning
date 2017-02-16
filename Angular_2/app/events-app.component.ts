import { Component } from '@angular/core'

@Component({
    selector: 'events-app',
    template: `
        <h2>Hello World from Component</h2>   
        <nav-bar></nav-bar>
        <router-outlet></router-outlet>
        `
})

export class EventsAppComponent{

}