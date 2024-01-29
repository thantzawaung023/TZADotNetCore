var blog = "tblName";
var editId = null;

var sumbitHandler = () => {
  if (editId == null) {
    createData();
  } else {
    updateData();
  }

  readData();
};

var createData = () => {
  var lst = [];
  if (localStorage.getItem(blog) !== null) {
    lst = JSON.parse(localStorage.getItem(blog));
  }
  //   console.log(lst);
  //   console.log(localStorage.getItem(blog));
  var blogName = $("#blogName").val();
  let data = {
    id: uuidv4(),
    Name: blogName,
  };
  lst.push(data);
  localStorage.setItem(blog, JSON.stringify(lst));
  $("#blogName").val("");
  $("#blogName").focus();
  successMessage("Saving Successful.");
  //   alert("Saving Successful.");
};

var readData = () => {
  if (JSON.parse(localStorage.getItem(blog)).length == 0) {
    trow = ``;
    $("#tbody").html(trow);
    return;
  }
  lst = JSON.parse(localStorage.getItem(blog));
  var trow = ``;
  var count = 0;
  lst.map((item) => {
    trow += `<tr>
    <th scope="row"><button type="button" class="btn btn-info" onclick="editData('${
      item.id
    }')"><i class="fa-solid fa-pen-to-square"></i></button>
    <button type="button" class="btn btn-danger" onclick="deleteData('${
      item.id
    }')"><i class="fa-solid fa-trash"></i></button></th>
    <td>${++count}</td>
    <td>${item.Name}</td>
  </tr>`;

    $("#tbody").html(trow);
    editId = null;
  });
};

var editData = (id) => {
  if (localStorage.getItem(blog) == null) return;

  if (localStorage.getItem(blog) !== null) {
    lst = JSON.parse(localStorage.getItem(blog));
  }

  var result = lst.filter((x) => {
    return x.id == id;
  });
  if (result.length == 0) {
    alert("No data Found.");
    return;
  }
  var item = result[0];
  editId = item.id;
  console.log("edit id", editId);
  $("#blogName").val(item.Name);
};

var updateData = () => {
  var lst = [];
  if (localStorage.getItem(blog) !== null) {
    lst = JSON.parse(localStorage.getItem(blog));
  }
  let index = lst.findIndex((x) => x.id == editId);
  lst[index].Name = $("#blogName").val();
  localStorage.setItem(blog, JSON.stringify(lst));
  $("#blogName").val("");
  //   alert("Updating Successful.");
  successMessage("Updating Successful.");
};

var deleteData = (id) => {
  //   var comfirm = confirm("are you sure want to delete!");
  comfirmMessage("are you sure want to delete ?").then(function (result) {
    if (!result) return;
    var lst = [];
    if (localStorage.getItem(blog) !== null) {
      lst = JSON.parse(localStorage.getItem(blog));
    }
    console.log("a", lst);
    var result = lst.filter((x) => x.id != id);
    localStorage.setItem(blog, JSON.stringify(result));
    readData();
    //   alert("Delete Successful.");
    successMessage("Deleting Successful.");
  });
};

//refresh readData
readData();

//uuid
function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
    ).toString(16)
  );
}
//uuid
