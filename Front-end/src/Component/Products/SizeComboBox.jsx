import React, { useEffect, useState } from "react";
import "react-widgets/dist/css/react-widgets.css";
import Combobox from "react-widgets/lib/Combobox";
import {
  CFormGroup,
  CInputGroup,
  CLabel,

} from "@coreui/react";

 function SizeComboBox({size,inputList,setInputList,locate}) {
  const [sizes, setSizeData] = useState([{ sizeid: "", quantity: "" }]);
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

  const  handleSizeChange = async (e, index,locate) => {
    const list = [...sizes]
    list[index]['sizeid'] = e;
    await setSizeData(list);
   setInputList([{...inputList,sizes: sizes}])
  };
  return (
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

          onChange={(e) => handleSizeChange(e.sizeId, p)}
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

  )
}

export default SizeComboBox
