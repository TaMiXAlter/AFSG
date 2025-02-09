import cv2
import mediapipe as mp
import socket
mp_drawing = mp.solutions.drawing_utils          
mp_drawing_styles = mp.solutions.drawing_styles  
mp_pose = mp.solutions.pose                     

mp_modelpath = 'model\pose_landmarker_lite.task'
#WebCam 需要根據電腦調整
Camera = cv2.VideoCapture(6)

# Communication
IP = "127.0.0.1"
Port = 6500
sock = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)


#MedePi
with mp_pose.Pose(
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5) as pose:

    if not Camera.isOpened():
        print("Cannot open camera")
        exit()
        
    while True:
        # 取得pose 資料 
        ret, img = Camera.read()
        if not ret:
            print("Cannot receive frame")
            break
        img = cv2.resize(img,(520,300))              
        imgRGB = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)  
        results = pose.process(imgRGB)             
        
        if not results:
            print("Cannot receive result")
            break
        
      
        
        
        if results.pose_landmarks is not None:
            # 整理 pose landmark
            pose_landmarks = []
            for lm in results.pose_landmarks.landmark:
                pose_landmarks.extend([lm.x, 1 - lm.y, lm.z])

            # 以 UDP 送出資料
            sock.sendto(str.encode(str(pose_landmarks)), (IP, Port))
            
              # 顯示pose點 
            mp_drawing.draw_landmarks(
                img,
                results.pose_landmarks,
                mp_pose.POSE_CONNECTIONS,
                landmark_drawing_spec=mp_drawing_styles.get_default_pose_landmarks_style())
            cv2.imshow('MeMePi', img)
        else:
            print("No pose landmarks detected")  # Debug 訊息

        
Camera.release()
cv2.destroyAllWindows()