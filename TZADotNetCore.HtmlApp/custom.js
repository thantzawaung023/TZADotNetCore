var successMessage = (message) => {
  Swal.fire({
    icon: "success",
    title: message,
    showConfirmButton: false,
    timer: 1500,
  });
};

var comfirmMessage = (message) => {
  return new Promise(function (myResolve, myReject) {
    Swal.fire({
      title: "Comfirm",
      text: message,
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes",
    }).then((result) => {
      return myResolve(result.isConfirmed);
    });
  });
};
