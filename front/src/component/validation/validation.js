import * as yup from 'yup';
export const createEmployee = yup.object({
    fName:yup.string().required('User Name is required').min(3,'your user name must have at least 3 characters').max(30,'your user name must have at most 30 characters'),
    lName:yup.string().required('User Name is required').min(3,'your user name must have at least 3 characters').max(30,'your user name must have at most 30 characters'),
    email:yup.string().required('Email is required').min(6,'your Email must have at least 6 characters').max(30,'your Email must have at most 30 characters'),
    password:yup.string().required('Password is required').min(6,'your Password must have at least 6 characters').max(30,'your Password must have at most 30 characters'),
    phoneNumber:yup.string().required('Phone Number is required').min(6,'your phone must have at least 6 characters').max(30,'your Password must have at most 30 characters'),
    address:yup.string().required('Address is required').min(6,'your address must have at least 6 characters').max(30,'your Password must have at most 30 characters'),
 })

 

 export const createCourse = yup.object({
    name:yup.string().required('Name is required').min(3,'Course Name must have at least 3 characters').max(30,'Course Name must have at most 30 characters'),
    price:yup.string().required('Price is required'),
    category:yup.string().required('Category is required').min(3,'Course Category must have at least 6 characters').max(30,'Course Category must have at most 30 characters'),
    subAdminId:yup.string().required('SubAdmin Id is required'),
    instructorId:yup.string().required('Instructor Id is required'),
    description:yup.string().required('Description is required').min(6,'Course Description must have at least 6 characters').max(30,'Course Description must have at most 30 characters'),
    //imageUrl: yup.mixed().required('Image is required'),
 })

 

 export const createEvent = yup.object({
   name:yup.string().required('Name is required').min(3,'Course Name must have at least 3 characters').max(30,'Course Name must have at most 30 characters'),
   content:yup.string().required('content is required').min(3,'Course content must have at least 6 characters').max(30,'Course content must have at most 30 characters'),
   category:yup.string().required('Category is required').min(3,'Course Category must have at least 6 characters').max(30,'Course Category must have at most 30 characters'),
   subAdminId:yup.string().required('SubAdmin Id is required'),
})