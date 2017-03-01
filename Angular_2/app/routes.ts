import {Routes} from '@angular/router'  //Adds intelisense for Routes
import {Error404Component} from './errors/404.component'

import {
    EventsListComponent,
    EventThumnailComponent,    
    EventService,
    EventDetailsComponent,    
    CreateEventComponent,
    EventRouteActivator,
    EventsListResolverService
} from './events/index'



export const appRoutes: Routes = [
    {path: 'events', component: EventsListComponent, resolve: {events : EventsListResolverService}},
    {path: 'events/new', component: CreateEventComponent, canDeactivate:['canDeactivateCreateEvent']}, //Ensure this is processed before the :id route as this will match with :id as well
    {path: 'events/:id', component: EventDetailsComponent, canActivate: [EventRouteActivator]},  //The colon forces the parameter to be read from the URL and be called id    
    {path: '404', component:Error404Component},
    {path: '', redirectTo:'/events', pathMatch: 'full'},  //Full -extact match or Prefix - starts with    

    {path : 'user', loadChildren: 'app/user/user.module#UserModule'}
    
]

//square brackets are needed around [EventRouteActivator], for some reason
//resolve is passed an object called events that is set from the EventsListResolverService
//resolve adds them to a property on the route.

//When the path starts with user, load the UserModule from the supplied path