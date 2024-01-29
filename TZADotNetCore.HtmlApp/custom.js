var successMessage = (message) => {
  //   Swal.fire({
  //     icon: "success",
  //     title: message,
  //     showConfirmButton: false,
  //     timer: 1500,
  //   });
  Notiflix.Report.success("Notiflix Success", message, "Okay");
};

var comfirmMessage = (message) => {
  //   return new Promise(function (myResolve, myReject) {
  //     Swal.fire({
  //       title: "Comfirm",
  //       text: message,
  //       icon: "warning",
  //       showCancelButton: true,
  //       confirmButtonColor: "#3085d6",
  //       cancelButtonColor: "#d33",
  //       confirmButtonText: "Yes",
  //     }).then((result) => {
  //       return myResolve(result.isConfirmed);
  //     });
  //   });
  return new Promise(function (myResolve, myReject) {
    Notiflix.Confirm.show(
      "Confirm",
      message,
      "Yes",
      "No",
      function okCb() {
        myResolve(true);
      },
      function cancelCb() {
        myResolve(false);
      }
    );
  });
};
