import axios from 'axios';

const objectToFormData = require('object-to-formdata')

class CommonService {

  toListFormData(data) {
    const formData = objectToFormData(data, { indices: true });
    return formData;
  }

  toFormData(data) {
    const formData = objectToFormData(data);
    return formData;
  }

  dateTimeformat(date) {
    let d = new Date(date);
    return d.getFullYear() + "." + d.getMonth() + "." + d.getDate();
  }

  getToast(component) {
    return component.$swal.mixin({
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 3000
    });
  }

  getComfirm(component) {
    return component.$swal({
      title: 'Biztos benne?',
      text: "Biztosan törölni szeretné ezt a tevékenységet?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Igen, törlöm!',
      cancelButtonText: 'Mégsem'
    });
  }
}

export const commonService = new CommonService();
