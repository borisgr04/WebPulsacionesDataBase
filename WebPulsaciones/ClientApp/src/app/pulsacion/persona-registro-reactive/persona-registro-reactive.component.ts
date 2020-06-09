import { Component, OnInit } from '@angular/core';
import { Persona } from '../models/persona';
import { PersonaService } from '../../services/persona.service';
import { FormBuilder, Validators, AbstractControl, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertModalComponent } from '../../@base/alert-modal/alert-modal.component';
import { GrupoPersona } from '../models/grupo-persona';

@Component({
  selector: 'app-persona-registro-reactive',
  templateUrl: './persona-registro-reactive.component.html',
  styleUrls: ['./persona-registro-reactive.component.css']
})
export class PersonaRegistroReactiveComponent implements OnInit {
  grupo = new GrupoPersona();
  persona: Persona;
  formGroup: FormGroup;
  submitted = false;

  constructor(
    private personaService: PersonaService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal) { }

  ngOnInit() {
    this.buildForm();
    this.addVariasPersonas();
  }

  private buildForm() {
    this.persona = new Persona();
    this.persona.identificacion = '';
    this.persona.nombre = '';
    this.persona.edad = 0;
    this.persona.pulsacion = 0;
    this.persona.sexo = '';

    this.formGroup = this.formBuilder.group({
      identificacion: [this.persona.identificacion, Validators.required],
      nombre: [this.persona.nombre, Validators.required],
      sexo: [this.persona.sexo, [Validators.required, this.validaSexo]],
      edad: [this.persona.edad, [Validators.required, Validators.min(1)]]
    });
  }

  private validaSexo(control: AbstractControl) {
    const sexo = control.value;
    if (sexo.toLocaleUpperCase() !== 'M' && sexo.toLocaleUpperCase() !== 'F') {
      return {
        validSexo: true, messageSexo: 'Sexo no Valido'
      };
    }
    return null;
  }

  get control() {
    return this.formGroup.controls;
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.formGroup.invalid) {
      return;
    }
    this.add();
  }
  
  add() {
    this.persona = this.formGroup.value;
    this.personaService.post(this.persona).subscribe(p => {
      if (p != null) {

        const messageBox = this.modalService.open(AlertModalComponent)
        messageBox.componentInstance.title = "Resultado Operación";
        messageBox.componentInstance.message = 'Persona creada!!! :-)';

        this.persona = p;
      }
    });
  }

  onChangeSexo(value: string) {
    console.log("nuevo valor del select " + value);
  }

  onReset() {
    this.submitted = false;
  }

  addVariasPersonas() {

    let p1 = new Persona();
    this.grupo.personas = [];
    this.grupo.codigo = "202033";
    p1.identificacion = '123';
    p1.nombre = "aaa";
    p1.sexo = "M";
    p1.edad = 3;
    p1.pulsacion = 1000;
    this.grupo.personas.push(p1);

    let p2 = new Persona();
    p2.identificacion = '123';
    p2.nombre = "aaa";
    p2.sexo = "M";
    p2.edad = 9;
    p2.pulsacion = 1000;
    this.grupo.personas.push(p2);
    
  }

  sendGrupo()
  {
    this.personaService.postGrupo(this.grupo)
      .subscribe(p =>
      {
        if (p!=null) 
            alert(p);
      }
    );
  }
}
