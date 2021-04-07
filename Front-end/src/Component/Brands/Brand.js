import React,{useEffect,useState} from 'react'
import {PropTypes} from 'prop-types';
import { Button, Modal } from "react-bootstrap";
import { connect } from 'react-redux';
import {
  CCard,
  CCardBody,
  CCardHeader,
  CCol,
  CDataTable,
  CRow,
  CButton,
  CInput,
  CForm
} from '@coreui/react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus  } from "@fortawesome/free-solid-svg-icons";
import { getBrand,addBrand, deleteBrand,searchBrand } from './../../reducers/brandAction';


const fields = ['name']
const Brand = ({brands,getBrand,addBrand,deleteBrand,searchBrand}) => {
  useEffect(() => {
    getBrand()
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
    const newBrand ={
      name
    }
    addBrand(newBrand)
    setName('')
  }

}

const handleModalShow = (e) => {
  if (e) e.preventDefault();
  setModalShow(true);
};


const [searchQuery, setSearchQuery] = React.useState('');

const onChangeSearch = event =>{
  setSearchQuery(event.target.value)
}

const onSearch = event =>{
  event.preventDefault()
  if(searchQuery !== ''){
    searchBrand(searchQuery.toUpperCase())
  }
  else if (searchQuery === '')
  {
    getBrand()
  }

}




  return (
    <>

      <CRow>
        <CCol>
          <CCard>
            <CCardHeader >
              <strong>Brand Manage</strong>
              <button
                  className="btn btn-success"
                  data-toggle="modal"
                  data-target="#editModal"
                  style ={{float:'right'}}
                  onClick={() => handleModalShow(null, 0)}
                >
                  <FontAwesomeIcon icon={faPlus} /> Add
                </button>


          <div  className="justify-content-center" style={{float: 'right', marginRight: '20px'}}>
          <CForm inline onSubmit={onSearch}>
                  <CInput
                    className=""
                    placeholder="Search"
                    size="sm"

                    onChange={onChangeSearch}

                  />
                  <CButton color="light" className="my-2 my-sm-0 ml-3" type="submit">Search</CButton>
                </CForm>
          </div>


            </CCardHeader>

            <CCardBody>

            <CDataTable
              items={brands}
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
                    <td key = {item.brandId}  >
                    {item.name}
                    <div style={{float: 'right'}}>
                    <CButton block variant="outline" color="danger" onClick={e =>  window.confirm("Are you sure you wish to delete the brand?") && deleteBrand(item.brandId)} >Delete</CButton>
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
          <Modal.Title>Add Brand</Modal.Title>
        </Modal.Header>
        <form onSubmit={onFormSubmit}>
          <Modal.Body>
          <CInput type="text" onChange ={onNameChange} value = {name}  placeholder="Enter brand name.." />

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


Brand.propTypes = {
	brands: PropTypes.array.isRequired,
  getBrand: PropTypes.func.isRequired,
  addBrand:PropTypes.func.isRequired,
  deleteBrand:PropTypes.func.isRequired,
  searchBrand:PropTypes.func.isRequired
}

const mapStateToProps = state => ({
  brands: state.brand.brands
})

export default connect(mapStateToProps,{getBrand,addBrand,deleteBrand,searchBrand})  (Brand)
