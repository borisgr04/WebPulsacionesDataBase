import { Component, OnInit } from '@angular/core';
import { Persona } from '../models/persona';
import { PersonaService } from '../../services/persona.service';
import { FormBuilder, Validators, AbstractControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-persona-registro-reactive',
  templateUrl: './persona-registro-reactive.component.html',
  styleUrls: ['./persona-registro-reactive.component.css']
})
export class PersonaRegistroReactiveComponent implements OnInit {

  persona: Persona;
  formGroup: FormGroup;
  submitted = false;

  constructor(private personaService: PersonaService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.buildForm();
  }

  private buildForm() {
    this.persona = new Persona();
    this.persona.identificacion = '';
    this.persona.nombre = '';
    this.persona.edad = 0;
    this.persona.pulsacion = 0;
    this.persona.sexo = '';

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
        validSexo: true, messageSexo: 'Sexo no Valido' 	};
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
        alert('Persona creada!');
        this.persona = p;
      }
    });
  }

}
