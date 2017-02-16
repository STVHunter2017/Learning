import {Component, OnInit } from '@angular/core'
import {EventService} from './shared/event.service'
@Component ({    
    template: `
         <div>
            <h1>Upcoming Angular 2 Events</h1>
            <hr>  
            <div class="row">  
                <div *ngFor="let event of events" class="col-md-5" >         
                    <event-thumbnail [event]="event"></event-thumbnail>             
                </div>                         
            </div>
        `,    
})

//This generates a null exception <event-thumbnail></event-thumbnail>

export class EventsListComponent implements OnInit {
  events: any  
  constructor(private eventService : EventService){
    //this.events = this.eventService.getEvents()  - move from constructor to NgOnInit
  }

  ngOnInit()  {
    this.events = this.eventService.getEvents()
  }
}

//private is short for creating a local variable and populating it from from the constructor
//implements OnInit compiler safety

//selector: 'events-list', was removed as we use Routing to get to it not direct access