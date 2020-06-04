import { Component, OnInit } from '@angular/core';
import { Persona } from '../models/persona';
import { PersonaService } from '../../services/persona.service';
import { SignalRService } from '../../services/signal-r.service';

@Component({
  selector: 'app-persona-consulta',
  templateUrl: './persona-consulta.component.html',
  styleUrls: ['./persona-consulta.component.css']
})
export class PersonaConsultaComponent implements OnInit {
  searchText:string;
  personas:Persona[];
  constructor(private personaService: PersonaService, private signalRService: SignalRService) { }

  ngOnInit() {

    this.personaService.get().subscribe(result => {
      this.personas = result;
    });

    ///Se suscribe al servicio de signal r y cuando se regustr una nueva persona se agregará el registro nuevo al array personas
    this.signalRService.personaReceived.subscribe((persona: Persona) => {
      this.personas.push(persona);
    });
      
    }
}
