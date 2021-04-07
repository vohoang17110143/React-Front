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
  CInput
} from '@coreui/react'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus  } from "@fortawesome/free-solid-svg-icons";
import { getColors,addColor } from 'src/reducers/colorAction';

const fields = ['name']
export const Color = ({getColors,addColor,colors}) => {
  useEffect(() => {
    getColors()
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
    const newColor ={
      name
    }
    addColor(newColor)
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
        <CCol >
          <CCard>
            <CCardHeader>
              <strong>Color Manage</strong>
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
              items={colors}
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
                    <td key = {item.colorId}  >
                    {item.name}
                    <div style={{float: 'right'}}>
                    </div>
                    <div  style={{float: 'right',marginRight: '6px'}}>
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
          <Modal.Title>Add Color</Modal.Title>
        </Modal.Header>
        <form onSubmit={onFormSubmit}>
          <Modal.Body>
          <CInput type="text" onChange ={onNameChange} value = {name}  placeholder="Enter color name.." />

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


Color.propTypes = {
	colors: PropTypes.array.isRequired,
  getColors: PropTypes.func.isRequired,
  addColor:PropTypes.func.isRequired,
}

const mapStateToProps = state => ({
  colors: state.color.colors
})

export default connect(mapStateToProps,{addColor,getColors}) (Color)
