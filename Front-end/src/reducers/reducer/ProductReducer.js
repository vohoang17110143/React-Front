import {GET_PRODUCT,ADD_PRODUCT} from "./../type";
const initialState = {
  products: [],
};


const productReducer =(state = initialState, action) => {

  switch (action.type) {
    case GET_PRODUCT:
      return {
        ...state,
        products: action.payload
      };
      case ADD_PRODUCT:
        return {
          ...state,
          products:  [...state.products, action.payload]
        };
    default:
      return state
  }
}

export default productReducer
