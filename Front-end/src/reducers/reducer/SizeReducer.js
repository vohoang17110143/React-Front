import {GET_SIZE,MARK_SIZE} from "./../type";
const initialState = {
  sizes: [],
};

const sizeReducer =(state = initialState, action) => {

  switch (action.type) {
    case GET_SIZE:
      return {
        ...state,
        sizes: action.payload,
      };
    default:
      return state
  }
}

export default sizeReducer
