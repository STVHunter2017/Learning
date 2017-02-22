import {Component, Input } from '@angular/core'

@Component ({
    selector: 'event-thumbnail',
    template: `
         <div [routerLink]="['/events', event.id]" class="well hoverwell thumbnail">
                <h2>{{event?.name}}</h2>
                    <div [style.color]="event?.time ==='8:00 am' ? '#003300' : '#bbb'">Date : {{event?.date}}</div>
                    <div [ngClass]="getStartTimeClass()" [ngSwitch] = "event?.time"> 
                        Time : {{event?.time}}
                    
                        <span *ngSwitchCase="'8:00 am'" >(Early Start)</span>
                        <span *ngSwitchCase="'10:00 am'">(Late Start)</span>                        
                        <span *ngSwitchDefault>(Normal Start)</span>
                    </div>         
                    
                    <div>Price : £{{event?.price}}</div>         

                    <div>
                        <div *ngIf="event?.location">
                            <span>Location: {{event?.location?.address}}</span>                                                
                            <span class="pad-left">{{event?.location?.city}}, {{event.location?.country}}</span>
                        </div>                    
                    </div>                    
                    <div *ngIf="event?.onlineUrl">
                        Online URL : {{event?.onlineUrl}}
                    </div>
        </div>    
            `,
  styles:[`
    .green {color: green !important;}
    .bold {font-weight: bold;}
    .pad-left {margin-left: 10px;}   
     .well div {color: #bbb}
     .thumbnail {min-height :210px}
  `]  
})

export class EventThumnailComponent{
    @Input() event: any    

    getStartTimeClass(){
        if (this.event && this.event.time === '8:00 am')
            return ['green', 'bold']
        
        return []
}
}

//? is the safe navigation marker
//RouteLink takes a route and a list of arguments to the route.
//[class.green]="event?.time === '8:00am'" is a class binding
//!important; forces style to not be overridden, this is due to the CSS in existince in this app.
//ngClass creates a class to hold the styles and rules that apply
//ngClass requires a space separated list, an array or a class.
//class and NgClass play well together