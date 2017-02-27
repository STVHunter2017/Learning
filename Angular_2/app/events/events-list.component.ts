import {Component, OnInit } from '@angular/core'
import {EventService} from './shared/event.service'
import {ToastrService} from '../common/toastr-service'

@Component ({    
    template: `
         <div>
            <h1>Upcoming Angular 2 Events</h1>
            <hr>  
            <div class="row">  
                <div *ngFor="let event of events" class="col-md-5" >         
                    <event-thumbnail [event]="event"></event-thumbnail>
                    <button type="button" class="btn"  (click)="handleThumbnailClick(event.name)">Test</button>
                </div>                         
            </div>
        `,    
})

//This generates a null exception <event-thumbnail></event-thumbnail>
export class EventsListComponent implements OnInit {
  events: any  
  constructor(private eventService : EventService, private toastrService: ToastrService){
    //this.events = this.eventService.getEvents()  - move from constructor to NgOnInit
  }

  ngOnInit()  {
    this.eventService.getEvents().subscribe(events=> {this.events = events} ) //Eventually this will return and will populate events when it does.
  }

  handleThumbnailClick(eventName){
    this.toastrService.success(eventName)
  }
}

//private is short for creating a local variable and populating it from from the constructor
//implements OnInit compiler safety

//selector: 'events-list', was removed as we use Routing to get to it not direct access