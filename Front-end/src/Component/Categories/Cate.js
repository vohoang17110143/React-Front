import React,{useEffect,useState} from 'react'
import {PropTypes} from 'prop-types';
import { Button, Modal } from "react-bootstrap";
import { connect } from 'react-redux';
import { getCategory, addCategory, deleteCategory } from './../../reducers/categoryAction';
import {
  CCard,
  CCardBody,
  CCardHeader,
  CCol,
  CDataTable,
  CRow,
  CButton,
  CInput
} from '@coreui/react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus  } from "@fortawesome/free-solid-svg-icons";

const fields = ['name']
const Cate = ({categories,getCategory,addCategory,deleteCategory}) => {
  useEffect(() => {
    getCategory()
  }, []);

  const [name,setName] = useState('');
const [modalShow, setModalShow] = useState(false);
const handleModalClose = () => setModalShow(false);
const onNameChange = event =>{
  setName(event.target.value)
}
const onFormSubmit = event =>{
  event.preventDefault()
  if(name !== ''){
    const newCate ={
      name
    }
    addCategory(newCate)
    setName('')
  }

}

const handleModalShow = (e) => {
  if (e) e.preventDefault();
  setModalShow(true);
};





  return (
   <>
          <CRow>
        <CCol>
          <CCard>
            <CCardHeader>
              <strong>Categories Manage</strong>
              <button
                  className="btn btn-success"
                  data-toggle="modal"
                  data-target="#editModal"
                  style ={{float:'right'}}
                  onClick={() => handleModalShow(null, 0)}
                >
                  <FontAwesomeIcon icon={faPlus} /> Add
                </button>


            </CCardHeader>
            <CCardBody>
            <CDataTable
              items={categories}
              fields={fields}
              hover
              striped
              bordered
              size="sm"
              itemsPerPage={10}
              pagination
              scopedSlots = {{
                'name':
                  (item)=>(
                    <td key = {item.categoryId}  >
                    {item.name}
                    <div style={{float: 'right'}}>
                    <CButton block variant="outline" color="danger"  onClick={e =>  window.confirm("Are you sure you wish to delete the brand?") && deleteCategory(item.categoryId)}  >Delete</CButton>
                    </div>
                    <div  style={{float: 'right',marginRight: '6px'}}>
                    <CButton block variant="outline" color="primary"   >Edit</CButton>
                    </div>
                    </td>
                  )
              }
              }
            />
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>





      <Modal
        show={modalShow}
        onHide={handleModalClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header closeButton>
          <Modal.Title>Add Category</Modal.Title>
        </Modal.Header>
        <form onSubmit={onFormSubmit}>
          <Modal.Body>
          <CInput type="text" onChange ={onNameChange} value = {name}  placeholder="Enter category name.." />

          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={handleModalClose}>
              Close
            </Button>
            <Button variant="primary" type="submit">
              Save
            </Button>
          </Modal.Footer>
        </form>
      </Modal>



   </>

  )
}


Cate.propTypes = {
	categories: PropTypes.array.isRequired,
  getCategory: PropTypes.func.isRequired,
  addCategory:PropTypes.func.isRequired,
  deleteCategory:PropTypes.func.isRequired
}

const mapStateToProps = state => ({
  categories: state.cate.categories
})
export default connect(mapStateToProps,{getCategory,addCategory,deleteCategory}) (Cate)
