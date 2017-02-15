import {Component, Input } from '@angular/core'

@Component ({
    selector: 'event-thumbnail',
    template: `
         <div class="well hoverwell thumbnail">
                <h2>{{event?.name}}</h2>
                    <div>Date : {{event?.date}}</div>
                    <div>Time : {{event?.time}}</div>         
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
    .pad-left {margin-left: 10px;}   
     .well div {color: #bbb}
     .thumbnail {min-height :210px}
  `]  
})

export class EventThumnailComponent{
    @Input() event: any    
}

//? is the safe navigation marker