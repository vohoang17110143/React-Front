import React, { useEffect, useState } from "react";
import "react-widgets/dist/css/react-widgets.css";
import Combobox from "react-widgets/lib/Combobox";
import axios from "axios";
import "./addProduct.css";
import SizeComboBox from './SizeComboBox';
import {

  CButton,
  CCard,
  CCardBody,
  CCardHeader,
  CTextarea,
  CCol,
  CForm,
  CFormGroup,
  CInput,
  CInputGroup,
  CInputGroupPrepend,
  CLabel,
  CRow,
} from "@coreui/react";

const AddProduct = () => {
  const [image, setImage] = useState("");
  const [listImage, setListImage] = useState([""]);
  const [category, setCategory] = useState([]);
  const [brand, setBrand] = useState([]);
  const [color, setColor] = useState([]);
  const [size, setSize] = useState([]);
  const sexdata = ["Nam", "Nữ"];
//useState dữ liệu danh sách size
const [sizes, setSizeData] = useState([{ sizeid: "", quantity: "" }]);
  //useState màu sản phẩm cùng danh sách size
  const [inputList, setInputList] = useState([
    { colorId: "", image: null, sizes:[] },
  ]);
//useState product xuất ra
  const [productAdd, setProductAdd] = useState({
    name: "",
    price: "",
    sex: "",
    image: null,
    description: "",
    brandId: "",
    categoryId: "",
    colors:[],
  });

//#region info tong quat
//xử lý thêm cac thông tin tổng quát của form chính
  const handleComboboxChangeCategory = (data) => {
    setProductAdd({ ...productAdd, categoryId: data });
  };

  const handleComboboxChangeSex = (data) => {
    setProductAdd({ ...productAdd, sex: data });
  };

  const handleComboboxChangeBrand = (data) => {
    setProductAdd({ ...productAdd, brandId: data });
  };
//#endregion



const handleSizeAdd = () => {
  setSizeData([...sizes, { sizeid: "", quantity: "" }]);
};

const handleSizeRemove = (index) => {
  const list = [...sizes];
  list.splice(index,1);
 setSizeData(list)
};

const handleQuanityChange= (e, index) => {
const { name, value } = e.target;
const list = [...sizes];
list[index][name] = value;
setSizeData(list);
};

const handleSizeChange = (e, index,locate) => {
  const list = [...sizes];
  list[index]['sizeid'] = e;
  setSizeData(list);
  const list2 = [...inputList];
  list2[locate]['sizes'] = sizes;
  setInputList(list2)
};

  //xử lý thêm,xóa form color nhập mới
//#region
  const handleAddClick = () => {
    setInputList([...inputList, { colorId: "", image: null, sizes:[] }]);
    setSizeData([{ sizeid: "", quantity: "" }])
  };
  const handleChange = (e, index) => {
    const list = [...inputList];
    list[index]["colorId"] = e;
    setInputList(list);

  };

  const handleRemoveClick = (index) => {
    const list = [...inputList];
    list.splice(index, 1);
    setInputList(list);
  };
  //#endregion


  //Fecth api cần dùng
  //#region
  const fetchData = () => {
    const brandApi = axios.get("https://localhost:5001/api/v1/brands/");
    const categoryApi = axios.get("https://localhost:5001/api/v1/categories/");
    const colorsApi = axios.get("https://localhost:5001/api/v1/colors/");
    const sizeApi = axios.get("https://localhost:5001/api/sizes/");
    axios.all([categoryApi, brandApi, colorsApi, sizeApi]).then(
      axios.spread((...allData) => {
        const cateData = allData[0].data;
        const brandData = allData[1].data;
        const colorData = allData[2].data;
        const sizeData = allData[3].data;
        setCategory(cateData);
        setBrand(brandData);
        setColor(colorData);
        setSize(sizeData);
      })
    );
  };

  useEffect(() => {
    fetchData();
  }, []);
  //#endregion


//xử lý ảnh
//#region
  const imageHandler = (e) => {
    const reader = new FileReader();
    reader.onload = () => {
      if (reader.readyState === 2) {
        setImage(reader.result);
      }
    };
    reader.readAsDataURL(e.target.files[0]);
    setImage(e.target.files[0]);
  };

  const ListimageHandler = (e) => {
    const reader = new FileReader();
    reader.onload = () => {
      if (reader.readyState === 2) {
        setListImage(reader.result);
      }
    };
    reader.readAsDataURL(e.target.files[0]);
    setListImage(e.target.files[0]);
  };
  //#endregion

//console ra màn hình

  console.log(inputList);
  return (
    <>
      <CRow>
        <CCol xs="12">
          <CCard>
            <CCardHeader>Add Product</CCardHeader>

            <CCardBody>
              <CForm className="form-horizontal">
                <div
                  style={{ display: "flex", justifyContent: "space-between" }}
                >
                  <div className="phase1 ml-5">
                    <CFormGroup className="col-12">
                      <CLabel htmlFor="prependedInput">Name : </CLabel>
                      <div className="controls ">
                        <CInputGroup className="input-prepend">
                          <CInputGroupPrepend></CInputGroupPrepend>
                          <CInput
                            id="prependedInput"
                            size="16"
                            type="text"
                            value={productAdd.name}
                            onChange={(event) =>
                              setProductAdd({
                                ...productAdd,
                                name: event.target.value,
                              })
                            }
                          />
                        </CInputGroup>
                      </div>
                    </CFormGroup>

                    <CFormGroup className="col-12">
                      <CLabel htmlFor="prependedInput">Price : </CLabel>
                      <div className="controls ">
                        <CInputGroup className="input-prepend">
                          <CInputGroupPrepend></CInputGroupPrepend>
                          <CInput
                            id="prependedInput"
                            size="16"
                            type="number"
                            value={productAdd.price}
                            onChange={(event) =>
                              setProductAdd({
                                ...productAdd,
                                price: event.target.value,
                              })
                            }
                          />
                        </CInputGroup>
                      </div>
                    </CFormGroup>

                    <CFormGroup className="col-12">
                      <CLabel htmlFor="prependedInput">Sex : </CLabel>
                      <div className="controls ">
                        <CInputGroup className="input-prepend">
                          <Combobox
                            data={sexdata}
                            defaultValue="Select sex"
                            onChange={(param) => handleComboboxChangeSex(param)}
                          />
                        </CInputGroup>
                      </div>
                    </CFormGroup>

                    <CFormGroup className="col-12">
                      <CLabel htmlFor="prependedInput">Category : </CLabel>
                      <div className="controls ">
                        <CInputGroup className="input-prepend">
                          <Combobox
                            key={category.map((item) => item.categoryId)}
                            valueField="categoryId"
                            textField="name"
                            data={category}
                            defaultValue="Select Category"
                            onChange={(param) =>
                              handleComboboxChangeCategory(param.categoryId)
                            }
                          />
                        </CInputGroup>
                      </div>
                    </CFormGroup>

                    <CFormGroup className="col-12">
                      <CLabel htmlFor="prependedInput">Brand : </CLabel>
                      <div className="controls ">
                        <CInputGroup className="input-prepend">
                          <Combobox
                            key={brand.map((item) => item.brandId)}
                            valueField="brandId"
                            textField="name"
                            data={brand}
                            defaultValue="Select Brand"
                            onChange={(param) =>
                              handleComboboxChangeBrand(param.brandId)
                            }
                          />
                        </CInputGroup>
                      </div>
                    </CFormGroup>

                    <CFormGroup className="col-12">
                      <CLabel htmlFor="prependedInput">Description : </CLabel>
                      <div className="controls ">
                        <CInputGroup className="input-prepend">
                          <CTextarea
                            name="textarea-input"
                            id="textarea-input"
                            rows="9"
                            placeholder="Content..."
                            maxLength="100"
                            value={productAdd.description}
                            onChange={(event) =>
                              setProductAdd({
                                ...productAdd,
                                description: event.target.value,
                              })
                            }
                          />
                        </CInputGroup>
                      </div>
                    </CFormGroup>
                  </div>

                  <div className="phase2">
                    <div className="img-holder">
                      <img
                        src={image}
                        alt=""
                        id="img"
                        className="img-preview"
                      />
                    </div>
                    <input
                      type="file"
                      accept="image/*"
                      name="image-upload"
                      id="input"
                      onChange={imageHandler}
                    />
                  </div>
                </div>
              </CForm>
            </CCardBody>
          </CCard>
          <CCard></CCard>
        </CCol>
      </CRow>

      {inputList.map((x, i) => {
        return (
          <CRow key={i}>
            <CCol xs="12">
              <CCard>
                <CCardBody>
                  <CForm action="" method="post" className="form-horizontal">
                    <div
                      style={{
                        display: "flex",
                        justifyContent: "space-between",
                      }}
                    >
                      <div className="phase1">
                        <CFormGroup className="col-10">
                          <CLabel htmlFor="prependedInput">Color : </CLabel>
                          <div className="controls ">
                            <CInputGroup className="input-prepend">
                              <Combobox
                                key={color.map((item) => item.colorId)}
                                valueField="colorId"
                                textField="name"
                                data={color}
                                defaultValue="Select Color"
                                value={x.colorId}
                                onChange={(e) => handleChange(e.colorId, i)}
                              />
                            </CInputGroup>
                          </div>
                        </CFormGroup>
                        {/* <CFormGroup className="col-3">
                          <CLabel htmlFor="prependedInput">Size : </CLabel>
                          <div>
                            <CInputGroup className="input-prepend">
                              <ul className="sizeList">
                                {size.map((item,index2) => (
                                  <li
                                    key={index2}
                                    className="sizeList-item"
                                  >
                                    <label
                                      className="checkbox-inline"
                                      style={{ marginRight: "0.5em" }}
                                    >
                                      <input type="checkbox" name="sizeid" value={item.sizeId} onChange={e => handleSizeChange(e,index2,i)} />
                                    </label>
                                    {item.sizeNumber}
                                    <input  type="text" name="quantity"   className="quantity" />
                                  </li>
                                ))}
                              </ul>
                            </CInputGroup>
                          </div>
                        </CFormGroup> */}

                        <CFormGroup className="col-3">
                          <CLabel htmlFor="prependedInput">Size : </CLabel>
                          <div>
                            <CInputGroup className="input-prepend">
                              {sizes.map((y, p) => {
                                return (
                                  <div key={p} className="box">
                                    <Combobox
                                     className="mr10"
                                     key={size.map((item) => item.sizeId)}
                                valueField="sizeId"
                                textField="sizeNumber"
                                data={size}
                                defaultValue="Select Size"

                                onChange={(e) => handleSizeChange(e.sizeId, p,i)}
                                    />
                                    <input
                                      type="text"
                                      className="ml10"
                                      name="quantity"
                                      placeholder="Quantity"
                                      value={y.quantity}
                                      onChange={e => handleQuanityChange(e,p)}
                                    />

                                    {sizes.length !== 1 && (
                                      <input
                                        type="button"
                                        value="Remove"
                                        onClick={() => handleSizeRemove(p)}
                                      ></input>
                                    )}

                                    {sizes.length - 1 === p  && (
                                      <input
                                        type="button"
                                        value="Add"
                                        onClick={handleSizeAdd}
                                      ></input>
                                    )}
                                  </div>
                                );
                              })}
                            </CInputGroup>
                          </div>
                        </CFormGroup>

                      </div>

                      <div className="phase2">
                        <div className="img-holder">
                          <img
                            src={listImage}
                            alt=""
                            id="img"
                            className="img-preview"
                          />
                        </div>
                        <input
                          type="file"
                          accept="image/*"
                          name="image-upload"
                          id="input"
                          onChange={ListimageHandler}
                        />
                      </div>
                    </div>
                  </CForm>
                  {inputList.length !== 1 && (
                    <input
                      type="button"
                      value="Remove"
                      onClick={() => handleRemoveClick(i)}
                      className="RemoveForm"
                    ></input>
                  )}

                  {inputList.length - 1 === i && (
                    <input
                      type="button"
                      value="Add"
                      onClick={handleAddClick}
                      className="AddForm"
                    ></input>
                  )}
                </CCardBody>
              </CCard>
            </CCol>
          </CRow>
        );
      })}

      <div
        className="form-actions"
        style={{ float: "right", marginBottom: "1em" }}
      >
        <CButton type="submit" color="primary">
          Save
        </CButton>
        <CButton style={{ marginLeft: "10px" }} color="danger">
          Cancel
        </CButton>
      </div>c
    </>
  );
};

export default AddProduct;
