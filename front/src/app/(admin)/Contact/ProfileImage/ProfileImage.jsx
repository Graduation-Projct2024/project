// import React, { useEffect, useState } from 'react';

// function preloadImage(url, onSuccess, onError) {
//   const img = new Image();
//   img.src = url;
//   img.onload = onSuccess;
//   img.onerror = onError;
// }

// function ProfileImage({ contact }) {
//   const [imgSrc, setImgSrc] = useState("./user1.png");

//   useEffect(() => {
//     const imageUrl = contact.imageUrl ? contact.imageUrl: "./user1.png";
//     preloadImage(
//       imageUrl,
//       () => setImgSrc(imageUrl), // On success, set the image URL
//       () => setImgSrc("./user1.png") // On error, set the default image
//     );
//   }, [contact.imageUrl]);

//   return (
//     <img
//       src={imgSrc}
//       className="pho pb-3 img-fluid"
//       alt="Profile"
//     />
//   );
// }

// export default ProfileImage;

import React, { useEffect, useState } from 'react';

function ProfileImage({ imageUrl }) {
  const [imgSrc, setImgSrc] = useState("./user1.png");
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    const checkImageExists = async (url) => {
      try {
        const response = await fetch(url, { method: 'HEAD' });
        if (response.ok) {
          setImgSrc(url);
        } else {
          setImgSrc("./user1.png");
        }
      } catch {
        setImgSrc("./user1.png");
      }
      setLoaded(true);
    };

    if (imageUrl) {
      checkImageExists(imageUrl.replace(/\\/g, '/'));
    } else {
      setLoaded(true);
    }
  }, [imageUrl]);

  if (!loaded) return null; // Optionally, return a loader/spinner here

  return (
    <img
      src={imgSrc}
      className="pho pb-3 img-fluid"
      alt="Profile"
      onError={(e) => {
        e.target.onerror = null; // prevents looping
        e.target.src = "./user1.png";
      }}
    />
  );
}

export default ProfileImage;
