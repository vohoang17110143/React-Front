import React, { useEffect, useState } from "react";
import { PropTypes } from "prop-types";
import { connect } from "react-redux";

import {
  CCard,
  CCardBody,
  CCardHeader,
  CCol,
  CDataTable,
  CRow,
  CButton,
  CInput,
} from "@coreui/react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { getProduct } from "./../../reducers/productAction";
import { Link, Redirect } from "react-router-dom";

const fields = ["name","color" , "price", "sex", "image"];
const Product = ({ getProduct, products }) => {
  useEffect(() => {
    getProduct();
  }, []);

  return (
    <>
      <CRow>
        <CCol>
          <CCard>
            <CCardHeader>
              <strong>Product Manage</strong>

              <Link
                className="btn btn-success"
                data-toggle="modal"
                data-target="#editModal"
                style={{ float: "right" }}
                to="/dashboard/addproduct"
              >
                {" "}
                <FontAwesomeIcon icon={faPlus} />
                Add
              </Link>
            </CCardHeader>
            <CCardBody>
              <CDataTable
                items={products}
                fields={fields}
                hover
                striped
                bordered
                size="sm"
                itemsPerPage={10}
                pagination
                scopedSlots={
                  {
                  'image': (item) => (
                    <td key={item.categoryId}>
                      <img src={item.image} alt="product" />
                      <div style={{ float: "right" }}>
                        <CButton block variant="outline" color="danger">
                          Delete
                        </CButton>
                      </div>
                      <div style={{ float: "right", marginRight: "6px" }}>
                        <CButton block variant="outline" color="primary">
                          Edit
                        </CButton>
                      </div>
                    </td>
                  ),
                  'color':(item)=>(
                 <td>

                   {item.colorDTOs.map(color =>" " +color.name) +" "}

                 </td>
               )
                }
              }
              />
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </>
  );
};

Product.propTypes = {
  products: PropTypes.array.isRequired,
  getProduct: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({
  products: state.product.products,
});

export default connect(mapStateToProps, { getProduct })(Product);
