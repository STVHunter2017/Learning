import {Component } from '@angular/core'

@Component ({
    selector: 'events-list',
    template: `
         <div>
            <h1>Upcoming Angular 2 Events</h1>
            <hr>
            <event-thumbnail #thumbnail [event]="event1"></event-thumbnail>
            <button class="btn btn-primary" (click)="thumbnail.LogFoo()" >Log me some foo</button>
            <h3>{{thumbnail.someProperty}}</h3>
        </div>
        `        
})

export class EventsListComponent {
    event1 = {
        id :1,
        name: 'Angular Connnect',
        date: '21/05/2019',
        time: '10:00 am',
        price : 599.99,
        imageUrl : '/app/assets/images/angularconnect-shield.png',
        location: {
            address: '1057 DT',
            city: 'London',
            country: 'England'
        }
    }

}

