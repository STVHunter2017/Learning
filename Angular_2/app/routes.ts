import {Routes} from '@angular/router'  //Adds intelisense for Routes
import {EventsListComponent} from './events/events-list.component'
import {EventDetailsComponent} from './events/event-details/event-details.component'

export const appRoutes: Routes = [
    {path: 'events', component: EventsListComponent},
    {path: 'events/:id', component: EventDetailsComponent},
    {path: '', redirectTo:'/events', pathMatch: 'full'},  //Full -extact match or Prefix - starts with    
]