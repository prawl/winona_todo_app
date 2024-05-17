import { Component, Inject, Input, OnInit } from '@angular/core';
import {
  MatSnackBarRef,
  MAT_SNACK_BAR_DATA,
} from '@angular/material/snack-bar';
import { AlertType } from '../../services/snack-bar.service';

export class MsgData {
  constructor(public type: string, public message: string) {}
}

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss'],
})
export class MessageComponent implements OnInit {
  @Input() data: MsgData;
  public snackBarClass: string;
  public iconUrl: string;

  constructor(
    @Inject(MAT_SNACK_BAR_DATA) response: MsgData,
    public snackBarRef: MatSnackBarRef<MessageComponent>
  ) {
    if (response) {
      this.data = response;
    }
  }

  ngOnInit() {
    switch (this.data.type) {
      case AlertType.Error:
        this.snackBarClass = 'bg-red-600';
        this.iconUrl = `app/shared/assets/icons/alert-solid.svg`;
        break;
      case AlertType.Success:
        this.snackBarClass = 'bg-green-600';
        this.iconUrl = `app/shared/assets/icons/check-solid.svg`;
        break;
      case AlertType.Warning:
        this.snackBarClass = 'bg-yellow-600';
        this.iconUrl = `app/shared/assets/icons/warning-solid.svg`;
        break;
      default:
        this.snackBarClass = 'bg-slate-500';
        this.iconUrl = `app/shared/assets/icons/information-solid.svg`;
        break;
    }
  }

  close(): void {
    this.snackBarRef.dismiss();
  }
}
