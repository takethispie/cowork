import { Component, OnInit } from '@angular/core';
import {NgForm} from "@angular/forms";
import {Field} from "../../dynamic-form-builder/Field";

@Component({
  selector: 'app-dynamic-form-modal',
  templateUrl: './dynamic-form-modal.component.html',
  styleUrls: ['./dynamic-form-modal.component.scss'],
})
export class DynamicFormModalComponent implements OnInit {

  public fields: Field[] = [
    {
      Type: 'Text',
      Name: 'firstName',
      Label: 'First Name',
      Value: ""
    },
    {
      Type: 'Text',
      Name: 'lastName',
      Label: 'Last Name',
      Value: ""
    },
    {
      Type: 'Text',
      Name: 'email',
      Label: 'Email',
      Value: ""
    },
    {
      Type: "Radio",
      Name: "etudiant",
      Label: "Est Etudiant",
      Value: "non",
      Options: [
        {
          Name: "non",
          Label: "Non",
          Value: "non"
        },
        {
          Name: "oui",
          Label: "oui",
          Value: "oui"
        },
      ]
    }
  ];

  constructor() { }

  ngOnInit() {}

  FormSubmitted(event: any) {
    console.log(event);
  }
}
