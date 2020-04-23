import { Pipe, PipeTransform } from '@angular/core';
import { Persona } from '../pulsacion/models/persona';

@Pipe({
  name: 'filtroPersona'
})
export class FiltroPersonaPipe implements PipeTransform {

  transform(personas: Persona[], searchText: string): any {
    if (searchText == null) return personas;
        return personas.filter(p=> p.nombre.toLowerCase().indexOf(searchText.toLowerCase()) !== -1 );
    }
}
