import * as yup from 'yup';
export const createEmployee = yup.object({
    FName:yup.string().required('User Name is required').min(3,'your user name must have at least 3 characters').max(30,'your user name must have at most 30 characters'),
    LName:yup.string().required('User Name is required').min(3,'your user name must have at least 3 characters').max(30,'your user name must have at most 30 characters'),
    email:yup.string().required('Email is required').min(6,'your Email must have at least 6 characters').max(30,'your Email must have at most 30 characters'),
    password:yup.string().required('Password is required').min(3,'your Password must have at least 6 characters').max(30,'your Password must have at most 30 characters'),
    phoneNumber:yup.string().required('Phone Number is required').min(6,'your phone must have at least 6 characters').max(30,'your Password must have at most 30 characters'),
    address:yup.string().required('Address is required').min(6,'your address must have at least 6 characters').max(30,'your Password must have at most 30 characters'),
 })

 

 export const createCourse = yup.object({
    name:yup.string().required('Name is required').min(3,'Course Name must have at least 3 characters').max(30,'Course Name must have at most 30 characters'),
    price:yup.string().required('Price is required'),
    category:yup.string().required('Category is required').min(3,'Course Category must have at least 6 characters').max(100,'Course Category must have at most 100 characters'),
    limitNumberOfStudnet:yup.string().required('limitNumberOfStudnet Id is required'),
    SubAdminId:yup.string().required('SubAdmin Id is required'),
    InstructorId:yup.string().required('Instructor Id is required'),
    startDate:yup.string().required('startDate is required'),
    Deadline:yup.string().required('Deadline is required'),
    description:yup.string().required('Description is required').min(6,'Course Description must have at least 6 characters').max(10000,'Course Description must have at most 100000 characters'),
    //imageUrl: yup.mixed().required('Image is required'),
 })

 

 export const createEvent = yup.object({
   name:yup.string().required('Name is required').min(3,'Course Name must have at least 3 characters').max(30,'Course Name must have at most 30 characters'),
   content:yup.string().required('content is required').min(3,'Course content must have at least 6 characters').max(30,'Course content must have at most 30 characters'),
   category:yup.string().required('Category is required').min(3,'Course Category must have at least 6 characters').max(30,'Course Category must have at most 30 characters'),
   dateOfEvent:yup.string().required('Start Date is required'),
   SubAdminId:yup.string().required('SubAdmin Id is required'),
})
export const updateEmployee = yup.object({
   fName:yup.string().required('First Name is required'),
   lName:yup.string().required('Last Name is required'),
   email:yup.string().required('Email is required'),
   address:yup.string().required('Address is required'),
   phoneNumber:yup.string().required('Phone number is required'),
})
export const editProfile = yup.object({
   FName:yup.string().required('First Name is required'),
   LName:yup.string().required('Last Name is required'),
   
})
export const editCourse = yup.object({
   InstructorId:yup.string().required('InstructorId is required'),
   startDate:yup.string().required('startDate is required'),
   Deadline:yup.string().required('Deadline is required'),
   limitNumberOfStudnet:yup.string().required('limitNumberOfStudnet is required'),
   description:yup.string().required('description is required'),
})

export const addWeeklyHours = yup.object({
   startTime:yup.string().required('Start time is Required'),
   endTime:yup.string().required('End time is Required'),
})
export const addSkills = yup.object({
   skillName:yup.string().required('Skill name is Required'),
})