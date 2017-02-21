import {Routes} from '@angular/router'  //Adds intelisense for Routes
import {EventsListComponent} from './events/events-list.component'
import {EventDetailsComponent} from './events/event-details/event-details.component'
import {CreateEvent} from './events/create-event-component'
import {Error404Component} from './errors/404.component'
import {EventRouteActivator} from './events/event-details/event-route-activator.service'

export const appRoutes: Routes = [
    {path: 'events', component: EventsListComponent},
    {path: 'events/new', component: CreateEvent}, //Ensure this is processed before the :id route as this will match with :id as well
    {path: 'events/:id', component: EventDetailsComponent, canActivate: [EventRouteActivator]},  //The colon forces the parameter to be read from the URL and be called id    
    {path: '404', component:Error404Component},
    {path: '', redirectTo:'/events', pathMatch: 'full'},  //Full -extact match or Prefix - starts with    
    
]

//square brackets are needed around [EventRouteActivator], for some reason